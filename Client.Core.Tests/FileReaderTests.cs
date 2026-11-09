using System.IO.Compression;
using System.Text;
using PayrollEngine.Client.Exchange;
using Xunit;

namespace PayrollEngine.Client.Core.Tests;

public class FileReaderTests : IDisposable
{
    // ReSharper disable once StringLiteralTypo
    private readonly string directory = Path.Combine(Path.GetTempPath(), "clientcoretest-" + Guid.NewGuid().ToString("N"));

    private const string JsonExchange = """
        {
          "tenants": [
            {
              "identifier": "Tenant.A",
              "regulations": [ { "name": "Regulation.Json" } ]
            }
          ]
        }
        """;

    private const string YamlExchange = """
        tenants:
        - identifier: Tenant.A
          regulations:
          - name: Regulation.Yaml
        - identifier: Tenant.B
        """;

    public FileReaderTests()
    {
        Directory.CreateDirectory(directory);
    }

    private string Archive(params (string Name, string Content)[] entries)
    {
        var path = Path.Combine(directory, "exchange.zip");
        using var archive = ZipFile.Open(path, ZipArchiveMode.Create);
        foreach (var (name, content) in entries)
        {
            var entry = archive.CreateEntry(name);
            using var stream = entry.Open();
            stream.Write(Encoding.UTF8.GetBytes(content));
        }
        return path;
    }

    [Fact]
    public async Task Archive_WithJsonEntry_IsRead()
    {
        var path = Archive(("lookups.json", JsonExchange));

        var exchange = await FileReader.ReadAsync<Model.Exchange>(path);

        var tenant = Assert.Single(exchange.Tenants);
        Assert.Equal("Tenant.A", tenant.Identifier);
        Assert.Equal("Regulation.Json", Assert.Single(tenant.Regulations).Name);
    }

    [Fact]
    public async Task Archive_WithYamlEntry_ReadsTheEntryContent()
    {
        var path = Archive(("lookups.yaml", YamlExchange));

        var exchange = await FileReader.ReadAsync<Model.Exchange>(path);

        Assert.Equal(["Tenant.A", "Tenant.B"], exchange.Tenants.Select(x => x.Identifier));
        Assert.Equal("Regulation.Yaml", Assert.Single(exchange.Tenants[0].Regulations).Name);
    }

    [Fact]
    public async Task Archive_WithJsonAndYamlEntries_MergesTheTenants()
    {
        var path = Archive(("a.json", JsonExchange), ("b.yml", YamlExchange), ("readme.txt", "ignored"));

        var exchange = await FileReader.ReadAsync<Model.Exchange>(path);

        Assert.Equal(2, exchange.Tenants.Count);
        var tenant = exchange.Tenants.Single(x => x.Identifier == "Tenant.A");
        Assert.Equal(["Regulation.Json", "Regulation.Yaml"], tenant.Regulations.Select(x => x.Name));
    }

    public void Dispose()
    {
        try { Directory.Delete(directory, true); }
        catch (IOException) { /* best effort */ }
    }
}
