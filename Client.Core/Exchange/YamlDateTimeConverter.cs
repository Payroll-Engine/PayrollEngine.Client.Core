using System;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Custom YAML type converter for DateTime support</summary>
public class YamlDateTimeConverter : IYamlTypeConverter
{
    /// <inheritdoc />
    public bool Accepts(Type type)
    {
        return type == typeof(DateTime) || type == typeof(DateTime?);
    }

    /// <inheritdoc />
    public object ReadYaml(IParser parser, Type type, ObjectDeserializer serializer)
    {
        // scalar value
        var scalar = parser.Consume<Scalar>();
        var value = scalar.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, out var result))
        {
            throw new YamlException($"Cannot parse '{value}' as DateTime. Use ISO 8601 format: 2023-01-01T00:00:00Z");
        }

        result = result.ToUtc();
        return result;
    }

    /// <inheritdoc />
    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
    {
        if (value == null)
        {
            emitter.Emit(new Scalar(""));
        }
        else
        {
            var date = ((DateTime)value).ToUtc();
            string formattedDate = System.Text.Json.JsonSerializer.Serialize(date).Trim('"');
            emitter.Emit(new Scalar(formattedDate));
        }
    }
}