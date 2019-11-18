using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;
using Spyglass.SDK.Services;

namespace Spyglass.Server.Converters
{
    public class MetricProviderConverter : JsonConverter<Metric>
    {
        public override Metric Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
//            var doc = JsonDocument.ParseValue(ref reader);
//
//            var props = doc.RootElement.EnumerateObject().ToList();
//
//            var typeProperty = props.First(t => t.Name.Equals(nameof(Metric.ProviderType), StringComparison.OrdinalIgnoreCase));
//            
//            var typePropertyValue = typeProperty.Value.GetString();
//            var providerType = ProviderService.GetProvider(typePropertyValue);
//            
//            var providerProperty = props.First(t => t.Name.Equals(nameof(Metric.Provider), StringComparison.OrdinalIgnoreCase));
//
//            var providerObj = providerProperty.Value;
//            var obj = JsonSerializer.Deserialize(providerObj.GetRawText(), providerType);

            return JsonSerializer.Deserialize<Metric>(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, Metric value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<Metric>(writer, value);
        }
    }
}