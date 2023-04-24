using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class RegulationPermission : Model, IRegulationPermission
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
    public RegulationPermission(IRegulationPermission copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRegulationPermission compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IRegulationPermission compare) =>
        string.Equals(TenantIdentifier, compare?.TenantIdentifier) &&
        string.Equals(RegulationName, compare?.RegulationName) &&
        string.Equals(PermissionTenantIdentifier, compare?.PermissionTenantIdentifier) &&
        string.Equals(PermissionDivisionName, compare?.PermissionDivisionName);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{TenantIdentifier} {RegulationName} > {PermissionTenantIdentifier}:{PermissionDivisionName} {base.ToString()}";

}