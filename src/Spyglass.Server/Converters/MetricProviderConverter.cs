using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;
using Spyglass.SDK.Services;

namespace Spyglass.Server.Converters
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
                .FirstOrDefault(p => p.Name.Equals(nameof(Metric.ProviderType), StringComparison.OrdinalIgnoreCase));

            if (typeProperty == null)
                throw new InvalidOperationException($"Property {nameof(Metric.ProviderType)} is required");
            
            var typePropertyValue = typeProperty.First.Value<string>();
            var providerType = ProviderService.GetProvider(typePropertyValue);

            if (providerType == null)
                throw new InvalidOperationException($"Type {typePropertyValue} could not be resolved");

            JToken provider;
            if (jsonObject.TryGetValue(nameof(Metric.Provider), StringComparison.OrdinalIgnoreCase, out provider))
            {
                ((JProperty)provider.Parent).Remove();
                using (var subReader = jsonObject.CreateReader())
                {
                    serializer.Populate(subReader, metric);
                }

                metric.Provider = (IMetricValueProvider)provider.ToObject(providerType);
            }

            return metric;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Metric);
        }
    }
}