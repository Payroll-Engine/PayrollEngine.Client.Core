using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollEngine.Client.Model;

/// <summary>A case value from a case change used in national, company and employee case</summary>
public class CaseChangeCaseValue : CaseValue, ICaseChangeCaseValue
{
    /// <inheritdoc/>
    [Required]
    public int CaseChangeId { get; set; }

    /// <inheritdoc/>
    public DateTime CaseChangeCreated { get; set; }

    /// <inheritdoc/>
    [Required]
    public int UserId { get; set; }

    /// <inheritdoc/>
    [StringLength(128)]
    public string UserIdentifier { get; set; }

    /// <inheritdoc/>
    public string Reason { get; set; }

    /// <inheritdoc/>
    public string ValidationCaseName { get; set; }

    /// <inheritdoc/>
    public CaseCancellationType CancellationType { get; set; }

    /// <inheritdoc/>
    public int? CancellationId { get; set; }

    /// <inheritdoc/>
    public int Documents { get; set; }
}