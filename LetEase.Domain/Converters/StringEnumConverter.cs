using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetEase.Domain.Converters
{
	public class StringEnumConverter<T> : JsonConverter<T> where T : struct, Enum
	{
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string value = reader.GetString();
			return Enum.Parse<T>(value, true);
		}

		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
