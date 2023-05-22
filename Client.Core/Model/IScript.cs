using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll script client object</summary>
public interface IScript : IModel, IKeyEquatable<IScript>
{
    /// <summary>The script name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The scripting function types</summary>
    List<FunctionType> FunctionTypes { get; set; }

    /// <summary>The script content</summary>
    string Value { get; set; }

    /// <summary>The script file name</summary>
    string ValueFile { get; set; }

    /// <summary>The override type</summary>
    public OverrideType OverrideType { get; set; }
}