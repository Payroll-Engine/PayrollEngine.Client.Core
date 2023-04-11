using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Script;

/// <summary>The script export context</summary>
public class ScriptExportContext
{
    /// <summary>The tenant</summary>
    public Tenant Tenant { get; set; }

    /// <summary>The user</summary>
    public User User { get; set; }

    /// <summary>The employee</summary>
    public Employee Employee { get; set; }

    /// <summary>The payroll</summary>
    public Payroll Payroll { get; set; }

    /// <summary>The regulation</summary>
    public Regulation Regulation { get; set; }

    /// <summary>The export mode</summary>
    public ScriptExportMode ExportMode { get; set; }

    /// <summary>The export object</summary>
    public ScriptExportObject ScriptObject { get; set; }

    /// <summary>The target c# namespace</summary>
    public string Namespace { get; set; }
}