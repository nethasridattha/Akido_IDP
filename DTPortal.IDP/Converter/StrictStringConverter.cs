using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace DTPortal.IDP.Converter
{
    // Custom converters for strict type checking
    public class StrictStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(string);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
                throw new JsonSerializationException($"Expected string but got {reader.TokenType}");
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }

    public class StrictIntConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(int);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new JsonSerializationException($"Expected integer but got {reader.TokenType}");
            return Convert.ToInt32(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }

    public class StrictBoolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(bool);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Boolean)
                throw new JsonSerializationException($"Expected boolean but got {reader.TokenType}");
            return Convert.ToBoolean(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }

}

