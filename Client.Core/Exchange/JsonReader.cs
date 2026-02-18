using System;
using System.IO;
using System.Threading.Tasks;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Read model from a JSON file</summary>
public static class JsonReader
{
    /// <summary>Read object from JSON file</summary>
    /// <param name="fileName">Name of the file</param>
    public static async Task<T> FromFileAsync<T>(string fileName) where T : class
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // ensure full path
        fileName = Path.GetFullPath(fileName);

        // import file
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Missing exchange JSON file {fileName}.");
        }

        // read json file
        var json = await File.ReadAllTextAsync(fileName);
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new PayrollException($"Invalid exchange JSON file {fileName}.");
        }

        // convert
        var obj = FromJson<T>(json);
        return obj ?? throw new PayrollException($"Invalid exchange model JSON file {fileName}.");
    }

    /// <summary>Read object from JSON string</summary>
    /// <param name="json">Object JSON</param>
    public static T FromJson<T>(string json) where T : class
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new PayrollException("Invalid exchange json.");
        }
        var obj = DefaultJsonSerializer.Deserialize<T>(json);
        return obj;
    }
}