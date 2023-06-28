
namespace PayrollEngine.Client.Model;

/// <summary>
/// Report templates query
/// </summary>
public class ReportTemplateQuery : Query
{
    /// <summary>
    /// Report culture
    /// </summary>
    public string Culture { get; set; }

    /// <summary>
    /// Exclude report content
    /// </summary>
    public bool ExcludeContent { get; set; }
}