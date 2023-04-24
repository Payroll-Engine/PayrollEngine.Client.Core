using System.Collections.Generic;
using PayrollEngine.Data;

namespace PayrollEngine.Client.Model;

/// <summary>The report client object</summary>
public interface IReport : IModel, IAttributeObject, IKeyEquatable<IReport>
{
    /// <summary>The payroll result report name</summary>
    string Name { get; set; }

    /// <summary>The localized payroll result report names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The payroll result report description</summary>
    string Description { get; set; }

    /// <summary>The localized payroll result report descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The report category</summary>
    string Category { get; set; }

    /// <summary>The report attribute mode</summary>
    ReportAttributeMode AttributeMode { get; set; }

    /// <summary>The report queries, key is the query name and value the api operation name</summary>
    Dictionary<string, string> Queries { get; set; }

    /// <summary>The report data relations, based on the queries</summary>
    List<DataRelation> Relations { get; set; }

    /// <summary>The report build expression</summary>
    string BuildExpression { get; set; }

    /// <summary>The report build expression file</summary>
    string BuildExpressionFile { get; set; }

    /// <summary>The report start expression</summary>
    string StartExpression { get; set; }

    /// <summary>The report start expression file</summary>
    string StartExpressionFile { get; set; }

    /// <summary>The report end expression</summary>
    string EndExpression { get; set; }

    /// <summary>The report end expression file</summary>
    string EndExpressionFile { get; set; }

    /// <summary>The report clusters</summary>
    List<string> Clusters { get; set; }
}