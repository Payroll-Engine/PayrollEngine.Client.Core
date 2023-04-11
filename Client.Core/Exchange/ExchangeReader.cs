using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using PayrollEngine.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Read exchange payroll from file</summary>
public static class ExchangeReader
{
    /// <summary>Reads the specified file name</summary>
    /// <param name="fileName">Name of the file</param>
    /// <param name="namespace">The target namespace</param>
    /// <returns>The payroll provider</returns>
    public static async Task<Model.Exchange> ReadAsync(string fileName, string @namespace = null)
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
            throw new PayrollException($"Missing exchange file {fileName}");
        }

        Model.Exchange exchange;
        var extension = Path.GetExtension(fileName);
        if (".json".Equals(extension))
        {
            exchange = await ExchangeFromJsonAsync(fileName);
        }
        else
        {
            exchange = await ExchangeFromArchiveAsync(fileName);
        }
        if (exchange == null)
        {
            throw new PayrollException($"Invalid exchange content in file {fileName}");
        }

        // namespace change
        if (!string.IsNullOrWhiteSpace(@namespace) && exchange.Tenants != null)
        {
            exchange.ChangeNamespace(@namespace);
        }

        return exchange;
    }

    private static async Task<Model.Exchange> ExchangeFromJsonAsync(string fileName)
    {
        var json = await File.ReadAllTextAsync(fileName);
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new PayrollException($"Invalid exchange JSON file {fileName}");
        }
        return JsonToExchange(json);
    }

    private static async Task<Model.Exchange> ExchangeFromArchiveAsync(string fileName)
    {
        try
        {
            var exchange = new Model.Exchange();
            using var archive = ZipFile.OpenRead(fileName);

            // process *.json archive entries ordered by name
            var entries = archive.Entries.Where(x => x.Name.EndsWith(".json")).OrderBy(x => x.Name).ToList();
            if (!entries.Any())
            {
                throw new PayrollException($"Empty exchange archive {fileName}");
            }

            // combine archive files into one exchange
            foreach (var entry in entries)
            {
                await using var stream = entry.Open();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var entryExchange = JsonToExchange(json);
                // merge entry exchange to one exchange
                exchange.Import(entryExchange);
            }

            return exchange;
        }
        catch (Exception exception)
        {
            throw new PayrollException($"Invalid exchange archive {fileName}", exception);
        }
    }

    private static Model.Exchange JsonToExchange(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new PayrollException("Invalid exchange json");
        }
        // exchange import from JSON
        var entryExchange = DefaultJsonSerializer.Deserialize<Model.Exchange>(json);
        return entryExchange;
    }
}