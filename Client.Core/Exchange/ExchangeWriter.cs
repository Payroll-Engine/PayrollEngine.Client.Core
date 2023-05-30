using System;
using System.IO;
using System.Threading.Tasks;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Write the exchange model to a JSON file</summary>
public static class ExchangeWriter
{
    /// <summary>Writes the specified provider</summary>
    /// <param name="exchange">The provider</param>
    /// <param name="fileName">Name of the file</param>
    public static async Task WriteAsync(Model.Exchange exchange, string fileName)
    {
        if (exchange == null)
        {
            throw new ArgumentNullException(nameof(exchange));
        }
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        // target folder
        var fileInfo = new FileInfo(fileName);
        if (!string.IsNullOrWhiteSpace(fileInfo.DirectoryName) && !Directory.Exists(fileInfo.DirectoryName))
        {
            Directory.CreateDirectory(fileInfo.DirectoryName);
        }

        // export file
        if (File.Exists(fileInfo.FullName))
        {
            File.Delete(fileInfo.FullName);
        }
        var json = DefaultJsonSerializer.Serialize(exchange);
        await File.WriteAllTextAsync(fileInfo.FullName, json);
    }
}