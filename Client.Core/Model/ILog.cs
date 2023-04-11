using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll log object</summary>
public interface ILog : IModel
{
    /// <summary>The log level (immutable)</summary>
    LogLevel Level { get; set; }

    /// <summary>The log message name (immutable)</summary>
    string Message { get; set; }
        
    /// <summary>The log user (immutable)</summary>
    string User { get; set; }

    /// <summary>The log error (immutable)</summary>
    string Error { get; set; }

    /// <summary>The log comment (immutable)</summary>
    string Comment { get; set; }

    /// <summary>The log owner (immutable)</summary>
    [StringLength(128)]
    string Owner { get; set; }

    /// <summary>The log owner type (immutable)</summary>
    [StringLength(128)]
    string OwnerType { get; set; }
}