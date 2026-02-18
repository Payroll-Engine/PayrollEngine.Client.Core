using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Custom YAML type converter for enum support</summary>
public class YamlEnumTypeConverter : IYamlTypeConverter
{
    /// <inheritdoc />
    public bool Accepts(Type type)
    {
        return type.IsEnum || (Nullable.GetUnderlyingType(type)?.IsEnum ?? false);
    }

    /// <inheritdoc />
    public object ReadYaml(IParser parser, Type type, ObjectDeserializer serializer)
    {
        var scalar = parser.Consume<Scalar>();
        var value = scalar.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var enumType = Nullable.GetUnderlyingType(type) ?? type;
        try
        {
            // exact match first
            foreach (var enumName in Enum.GetNames(enumType))
            {
                if (string.Equals(enumName, value, StringComparison.Ordinal))
                {
                    return Enum.Parse(enumType, enumName);
                }
            }

            // case-insensitive match second
            return Enum.Parse(enumType, value, ignoreCase: true);
        }
        catch (ArgumentException)
        {
            var validValues = string.Join(", ", Enum.GetNames(enumType));
            throw new YamlException($"Invalid enum value '{value}' for {enumType.Name}. Valid values: {validValues}");
        }
    }

    /// <inheritdoc />
    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
    {
        var enumValue = value?.ToString() ?? string.Empty;
        emitter.Emit(new Scalar(enumValue));
    }
}