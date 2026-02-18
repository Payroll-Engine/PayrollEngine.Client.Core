using System;
using System.IO;
using System.Threading.Tasks;
using PayrollEngine.IO;

namespace PayrollEngine.Client.Exchange;

/// <summary>Write exchange object to file</summary>
public static class FileWriter
{
    /// <summary>Writes object to file</summary>
    /// <param name="obj">The object to store</param>
    /// <param name="fileName">Name of the file</param>
    /// <remarks>Supported file types JSON/YAML</remarks>
    public static async Task Write<T>(T obj, string fileName) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        var extension = Path.GetExtension(fileName);

        // json
        if (string.Equals(extension, FileExtensions.Json, StringComparison.InvariantCultureIgnoreCase))
        {
            await JsonWriter.ToFileAsync(obj, fileName);
            return;
        }

        // yaml
        if (string.Equals(extension, FileExtensions.Yaml, StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(extension, FileExtensions.Yml, StringComparison.InvariantCultureIgnoreCase))
        {
            await YamlWriter.ToFileAsync(obj, fileName);
            return;
        }

        throw new PayrollException($"Unsupported file extension {extension}.");
    }
}