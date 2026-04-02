using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll client object</summary>
public interface IPayroll : IModel, IAttributeObject, IKeyEquatable<IPayroll>
{
    /// <summary>The payroll name</summary>
    string Name { get; set; }

    /// <summary>The localized payroll names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>Payroll description</summary>
    string Description { get; set; }

    /// <summary>The localized payroll descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>The division id</summary>
    int DivisionId { get; set; }

    /// <summary>The division name (client only)</summary>
    string DivisionName { get; set; }

    /// <summary>Typed cluster set name references for this payroll</summary>
    PayrollClusterSets ClusterSet { get; set; }

    /// <summary>Cluster sets</summary>
    List<ClusterSet> ClusterSets { get; set; }
}
