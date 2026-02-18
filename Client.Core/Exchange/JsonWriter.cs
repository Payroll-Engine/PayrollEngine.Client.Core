using System;
using System.IO;
using System.Threading.Tasks;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Write model to JSON file</summary>
public static class JsonWriter
{
    /// <summary>Writes the specified provider</summary>
    /// <param name="obj">The provider</param>
    /// <param name="fileName">Name of the file</param>
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
        var json = DefaultJsonSerializer.Serialize(obj);

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
            if (string.Equals(existing, json))
            {
                return;
            }
        }
        
        // export file
        if (File.Exists(fileInfo.FullName))
        {
            File.Delete(fileInfo.FullName);
        }

        // write file
        await File.WriteAllTextAsync(fileInfo.FullName, json);
    }
}