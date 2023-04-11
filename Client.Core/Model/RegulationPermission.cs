using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class RegulationPermission : Model, IRegulationPermission, IEquatable<RegulationPermission>
{
    /// <inheritdoc/>
    public int TenantId { get; set; }

    /// <inheritdoc/>
    public string TenantIdentifier { get; set; }

    /// <inheritdoc/>
    public int RegulationId { get; set; }

    /// <inheritdoc/>
    public string RegulationName { get; set; }

    /// <inheritdoc/>
    public int PermissionTenantId { get; set; }

    /// <inheritdoc/>
    public string PermissionTenantIdentifier { get; set; }

    /// <inheritdoc/>
    public int? PermissionDivisionId { get; set; }

    /// <inheritdoc/>
    public string PermissionDivisionName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public RegulationPermission()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationPermission(RegulationPermission copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <summary>Compare two objects</summary>
    /// <param name="compare">The object to compare with this</param>
    /// <returns>True for objects with the same data</returns>
    public virtual bool Equals(RegulationPermission compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{TenantIdentifier} {RegulationName} > {PermissionTenantIdentifier}:{PermissionDivisionName} {base.ToString()}";

}