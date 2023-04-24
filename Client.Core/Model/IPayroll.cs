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

    /// <summary>The calendar calculation mode</summary>
    CalendarCalculationMode CalendarCalculationMode { get; set; }

    /// <summary>The country name (client only)</summary>
    Country? CountryName { get; set; }

    /// <summary>The ISO 3166-1 country code, 0 for undefined</summary>
    int Country { get; set; }

    /// <summary>The case cluster set (undefined: all)</summary>
    string ClusterSetCase { get; set; }
        
    /// <summary>The case field cluster set (undefined: all)</summary>
    string ClusterSetCaseField { get; set; }

    /// <summary>The collector cluster set (undefined: all)</summary>
    string ClusterSetCollector { get; set; }

    /// <summary>The collector cluster set for retro payrun jobs (undefined: all)</summary>
    string ClusterSetCollectorRetro { get; set; }

    /// <summary>The wage type cluster set (undefined: all)</summary>
    string ClusterSetWageType { get; set; }

    /// <summary>The wage type cluster set for retro payrun jobs (undefined: all)</summary>
    string ClusterSetWageTypeRetro { get; set; }

    /// <summary>The case value cluster set (undefined: none, *: all)</summary>
    string ClusterSetCaseValue { get; set; }

    /// <summary>The wage type period result cluster set (undefined: none)</summary>
    string ClusterSetWageTypePeriod { get; set; }

    /// <summary>Cluster sets</summary>
    List<ClusterSet> ClusterSets { get; set; }
}