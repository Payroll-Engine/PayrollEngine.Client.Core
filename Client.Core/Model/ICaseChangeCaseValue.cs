using System;

namespace PayrollEngine.Client.Model;

/// <summary>The case value of case change (immutable)</summary>
public interface ICaseChangeCaseValue : ICaseValue
{
    /// <summary>The case change id</summary>
    int CaseChangeId { get; set; }

    /// <summary>The case change creation</summary>
    DateTime CaseChangeCreated { get; set; }

    /// <summary>The change user id</summary>
    int UserId { get; set; }

    /// <summary>The user unique identifier</summary>
    string UserIdentifier { get; set; }

    /// <summary>The change reason</summary>
    string Reason { get; set; }

    /// <summary>The validation case name</summary>
    string ValidationCaseName { get; set; }

    /// <summary>The cancellation type</summary>
    CaseCancellationType CancellationType { get; set; }

    /// <summary>The canceled case change id</summary>
    int? CancellationId { get; set; }
  
    /// <summary>The document count</summary>
    int Documents { get; set; }
}