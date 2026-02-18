using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.System.Text.Json;
using YamlDotNet.Serialization.NamingConventions;

namespace PayrollEngine.Client.Exchange;

/// <summary>Read model from a YAML file</summary>
public static class YamlReader
{
    private const string JsonSchemaMarker = "$schema: ";
    private const string YamlSchemaMarker = "# yaml-language-server: $schema=";

    // deserializer with converters
    private static IDeserializer Deserializer { get; } = new DeserializerBuilder()
        // json attribute support
        .AddSystemTextJson()
        // camelCase naming convention
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        // converters
        .WithTypeConverter(new YamlEnumTypeConverter())
        .WithTypeConverter(new YamlDateTimeConverter())
        .WithTypeConverter(new YamlDictionaryTypeConverter())
        .Build();

    /// <summary>Load YAML file and deserialize directly to Exchange object</summary>
    /// <param name="fileName">Name of the file</param>
    public static async Task<T> FromFileAsync<T>(string fileName) where T : class
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // ensure full path
        fileName = Path.GetFullPath(fileName);
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Missing exchange YAML file {fileName}.");
        }

        // import file
        var yaml = await File.ReadAllTextAsync(fileName);
        if (string.IsNullOrWhiteSpace(yaml))
        {
            throw new PayrollException($"Invalid exchange YAML file {fileName}.");
        }

        // convert
        var obj = FromYaml<T>(yaml);
        return obj ?? throw new PayrollException($"Invalid exchange model in YAML file {fileName}.");
    }

    /// <summary>Deserialize YAML to object</summary>
    /// <param name="yaml">Yaml content</param>
    public static T FromYaml<T>(string yaml) where T : class
    {
        if (string.IsNullOrWhiteSpace(yaml))
        {
            throw new PayrollException("Invalid exchange yaml.");
        }

        // schema
        if (yaml.StartsWith(YamlSchemaMarker))
        {
            yaml = yaml.Replace(YamlSchemaMarker, JsonSchemaMarker);
        }

        // deserialize
        return Deserializer.Deserialize<T>(yaml);
    }
}