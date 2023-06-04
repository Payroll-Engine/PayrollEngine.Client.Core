namespace PayrollEngine.Client.Model;

/// <summary>The payroll layer client object</summary>
public interface IPayrollLayer : IModel, IAttributeObject, IKeyEquatable<IPayrollLayer>
{
    /// <summary>The layer level</summary>
    int Level { get; set; }

    /// <summary>The layer priority (default: 1)</summary>
    int Priority { get; set; }

    /// <summary>The regulation name</summary>
    string RegulationName { get; set; }
}