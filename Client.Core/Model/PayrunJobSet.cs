using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>
/// A payrun job with its payroll result sets — used for archive restore and migration.
/// Only jobs with <see cref="PayrunJobStatus.Complete"/> or <see cref="PayrunJobStatus.Forecast"/>
/// are accepted on import.
/// </summary>
public class PayrunJobSet : PayrunJob
{
    /// <summary>The payroll result sets belonging to this job</summary>
    public List<PayrollResultSet> ResultSets { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunJobSet()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    public PayrunJobSet(PayrunJobSet copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }
}
