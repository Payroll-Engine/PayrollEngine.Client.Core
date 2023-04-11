
namespace PayrollEngine.Client.Model;

/// <summary>The report template client object</summary>
public interface IReportTemplate : IModel, IAttributeObject
{
    /// <summary>The report language</summary>
    Language Language { get; set; }

    /// <summary>The report content (client owned)</summary>
    string Content { get; set; }

    /// <summary>The report content file name (client only)</summary>
    string ContentFile { get; set; }

    /// <summary>The report content type</summary>
    string ContentType { get; set; }

    /// <summary>The report schema (client owned)</summary>
    string Schema { get; set; }

    /// <summary>The report schema file name (client only)</summary>
    string SchemaFile { get; set; }

    /// <summary>The report external resource</summary>
    string Resource { get; set; }
}