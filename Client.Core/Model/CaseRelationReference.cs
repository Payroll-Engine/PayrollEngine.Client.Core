using System;

namespace PayrollEngine.Client.Model;

/// <summary>Case relation reference</summary>
public class CaseRelationReference : IEquatable<CaseRelationReference>
{
    /// <summary>The relation source case name (immutable)</summary>
    public string SourceCaseName { get; set; }

    /// <summary>The relation source case slot</summary>
    public string SourceCaseSlot { get; set; }

    /// <summary>The relation target case name (immutable)</summary>
    public string TargetCaseName { get; set; }

    /// <summary>The relation target case slot</summary>
    public string TargetCaseSlot { get; set; }

    /// <summary>Default constructor</summary>
    public CaseRelationReference()
    {
    }

    /// <summary>Relation copy constructor</summary>
    /// <param name="copySource">The copy source</param>
    public CaseRelationReference(CaseRelationReference copySource) :
        this(copySource.SourceCaseName, copySource.SourceCaseSlot, copySource.TargetCaseName, copySource.TargetCaseSlot)
    {
    }

    /// <summary>Relation constructor</summary>
    /// <param name="sourceCaseName">The relation source case name</param>
    /// <param name="targetCaseName">The relation target case name</param>
    public CaseRelationReference(string sourceCaseName, string targetCaseName)
    {
        if (string.IsNullOrWhiteSpace(sourceCaseName))
        {
            throw new ArgumentException(nameof(sourceCaseName));
        }
        if (string.IsNullOrWhiteSpace(targetCaseName))
        {
            throw new ArgumentException(nameof(targetCaseName));
        }

        SourceCaseName = sourceCaseName;
        TargetCaseName = targetCaseName;
    }

    /// <summary>Relation with slot constructor</summary>
    /// <param name="sourceCaseName">The relation source case name</param>
    /// <param name="sourceCaseSlot">The relation source case slot</param>
    /// <param name="targetCaseName">The relation target case name</param>
    /// <param name="targetCaseSlot">The relation target case slot</param>
    public CaseRelationReference(string sourceCaseName, string sourceCaseSlot, string targetCaseName, string targetCaseSlot) :
        this(sourceCaseName, targetCaseName)
    {
        SourceCaseSlot = sourceCaseSlot;
        TargetCaseSlot = targetCaseSlot;
    }

    /// <inheritdoc/>
    public virtual bool Equals(CaseRelationReference compare) =>
        CompareTool.EqualProperties(this, compare);
}