using System.Collections.Generic;

namespace PayrollEngine.Client.Script;

/// <summary>Script for development</summary>
public class DevelopmentScript
{
    /// <summary>The scripting function types</summary>
    public List<FunctionType> FunctionTypes { get; set; }

    /// <summary>The scripting function type</summary>
    public FunctionType? FunctionType { get; set; }

    /// <summary>The owner id</summary>
    public int OwnerId { get; set; }

    /// <summary>The script name</summary>
    public string ScriptName { get; set; }

    /// <summary>The class name</summary>
    public string ClassName { get; set; }

    /// <summary>The script content</summary>
    public string Content { get; set; }
}