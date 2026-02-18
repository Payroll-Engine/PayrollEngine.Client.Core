using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.System.Text.Json;
using YamlDotNet.Serialization.NamingConventions;

namespace PayrollEngine.Client.Exchange;

/// <summary>Write model to YAML file</summary>
public static class YamlWriter
{
    private const string JsonSchemaMarker = "$schema: ";
    private const string YamlSchemaMarker = "# yaml-language-server: $schema=";

    // Serializer with converters
    private static ISerializer Serializer { get; } = new SerializerBuilder()
        // quote necessary values
        .WithQuotingNecessaryStrings()
        // json attribute support
        .AddSystemTextJson()
        // camelCase naming convention
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        // ignore values
        .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitEmptyCollections)
        // inspectors
        .WithTypeInspector(inner => new PropertyFilteringInspector(inner))
        // converters
        .WithTypeConverter(new YamlEnumTypeConverter())
        .WithTypeConverter(new YamlDateTimeConverter())
        .WithTypeConverter(new YamlDictionaryTypeConverter())
        .Build();

    /// <summary>Save obj object to YAML file</summary>
    public static async Task ToFileAsync<T>(T obj, string fileName) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // serialize
        var yaml = Serializer.Serialize(obj).Trim();

        // schema
        if (yaml.StartsWith(JsonSchemaMarker))
        {
            yaml = yaml.Replace(JsonSchemaMarker, YamlSchemaMarker);
        }

        // target folder
        var fileInfo = new FileInfo(fileName);
        if (!string.IsNullOrWhiteSpace(fileInfo.DirectoryName) && !Directory.Exists(fileInfo.DirectoryName))
        {
            Directory.CreateDirectory(fileInfo.DirectoryName);
        }

        // skip same existing file
        if (File.Exists(fileName))
        {
            var existing = await File.ReadAllTextAsync(fileName);
            if (string.Equals(existing, yaml))
            {
                return;
            }
        }

        // write file
        await File.WriteAllTextAsync(fileName, yaml);
    }
}