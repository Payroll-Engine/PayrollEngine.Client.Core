using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The employee set client object</summary>
public interface IEmployeeSet: IEmployee
{
    /// <summary>The employee case changes</summary>
    List<CaseChange> Cases { get; set; }

    /// <summary>The employee case values</summary>
    List<CaseValue> Values { get; set; }
}