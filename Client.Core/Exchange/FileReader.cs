using System;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Threading.Tasks;
using PayrollEngine.IO;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Exchange;

/// <summary>Read exchange mode from a file</summary>
public static class FileReader
{

    /// <summary>Read object from file</summary>
    /// <param name="fileName">Name of the file</param>
    /// <remarks>Supported file types are JSON/YAML and archives including JSON/YAML</remarks>
    public static async Task<T> Read<T>(string fileName) where T : class, new()
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
            throw new PayrollException($"Missing exchange file {fileName}.");
        }

        T obj;
        var extension = Path.GetExtension(fileName);

        // json
        if (string.Equals(extension, FileExtensions.Json, StringComparison.InvariantCultureIgnoreCase))
        {
            obj = await JsonReader.FromFileAsync<T>(fileName);
        }
        // yaml
        else if (string.Equals(extension, FileExtensions.Yaml, StringComparison.InvariantCultureIgnoreCase) ||
                 string.Equals(extension, FileExtensions.Yml, StringComparison.InvariantCultureIgnoreCase))
        {
            obj = await YamlReader.FromFileAsync<T>(fileName);
        }
        // archive
        else
        {
            obj = await ReadArchiveAsync<T>(fileName);
        }

        return obj ?? throw new PayrollException($"Invalid exchange content in file {fileName}.");
    }

    /// <summary>
    /// Read archive
    /// </summary>
    /// <param name="fileName">File to read</param>
    private static async Task<T> ReadArchiveAsync<T>(string fileName) where T : class, new()
    {
        var obj = new T();
        await using var archive = await ZipFile.OpenReadAsync(fileName);

        // process *.json archive entries ordered by name
        var entries = archive.Entries.OrderBy(x => x.Name).ToList();
        if (!entries.Any())
        {
            throw new PayrollException($"Empty exchange archive {fileName}.");
        }

        // import
        var importObj = obj as IImportObject<T>;

        // combine archive files into one exchange
        foreach (var entry in entries)
        {
            var info = new FileInfo(entry.FullName);
            if (string.IsNullOrWhiteSpace(info.Extension))
            {
                continue;
            }

            await using var stream = await entry.OpenAsync();
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();

            T newObj;
            // json
            if (string.Equals(info.Extension, FileExtensions.Json, StringComparison.InvariantCultureIgnoreCase))
            {
                newObj = JsonReader.FromJson<T>(content);
            }
            // yaml
            else if (string.Equals(info.Extension, FileExtensions.Yaml, StringComparison.InvariantCultureIgnoreCase) ||
                     string.Equals(info.Extension, FileExtensions.Yml, StringComparison.InvariantCultureIgnoreCase))
            {
                newObj = YamlReader.FromYaml<T>(fileName);
            }
            else
            {
                // ignore unsupported archive entries
                continue;
            }

            // import object
            importObj?.Import(newObj);
        }

        return obj;
    }
}