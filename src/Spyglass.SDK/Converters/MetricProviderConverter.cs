using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spyglass.SDK.Data;
using Spyglass.SDK.Providers;
using Spyglass.SDK.Services;

namespace Spyglass.SDK.Converters
{
    public class MetricProviderConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var metric = new Metric();
            JObject jsonObject = JObject.Load(reader);

            var typeProperty = jsonObject.Properties()
                .FirstOrDefault(p => p.Name.Equals(nameof(Metric.Type), StringComparison.OrdinalIgnoreCase));

            if (typeProperty == null)
                throw new InvalidOperationException($"Property {nameof(Metric.Type)} is required");
            
            var providerService = new ProviderService();
            var typePropertyValue = typeProperty.First.Value<string>();
            var providerType = providerService.GetProvider(typePropertyValue);

            if (providerType == null)
                throw new InvalidOperationException($"Type {typePropertyValue} could not be resolved");

            JToken provider;
            if (jsonObject.TryGetValue(nameof(Metric.ValueProvider), StringComparison.OrdinalIgnoreCase, out provider))
            {
                ((JProperty)provider.Parent).Remove();
                using (var subReader = jsonObject.CreateReader())
                {
                    serializer.Populate(subReader, metric);
                }

                metric.ValueProvider = (IMetricValueProvider)provider.ToObject(providerType);
            }

            return metric;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Metric);
        }
    }
}