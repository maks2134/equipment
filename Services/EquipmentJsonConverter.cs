using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using equipment_accounting_system.Models;

namespace equipment_accounting_system.Services
{

    public class EquipmentJsonConverter : JsonConverter<Equipment>
    {
        public override Equipment? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (root.TryGetProperty("portCount", out _) || root.TryGetProperty("connectionType", out _))
                {
                    return JsonSerializer.Deserialize<NetworkEquipment>(root.GetRawText(), options);
                }
                else if (root.TryGetProperty("processor", out _) || root.TryGetProperty("ram", out _))
                {
                    return JsonSerializer.Deserialize<ComputerEquipment>(root.GetRawText(), options);
                }
                else if (root.TryGetProperty("equipmentCategory", out _) || root.TryGetProperty("printSpeed", out _))
                {
                    return JsonSerializer.Deserialize<OfficeEquipment>(root.GetRawText(), options);
                }
                else
                {

                    return JsonSerializer.Deserialize<ComputerEquipment>(root.GetRawText(), options);
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Equipment value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
