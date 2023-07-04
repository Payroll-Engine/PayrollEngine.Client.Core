using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The tenant client object</summary>
public class RegulationShare : ModelBase, IRegulationShare
{
    /// <inheritdoc/>
    public int ProviderTenantId { get; set; }

    /// <inheritdoc/>
    public string ProviderTenantIdentifier { get; set; }

    /// <inheritdoc/>
    public int ProviderRegulationId { get; set; }

    /// <inheritdoc/>
    public string ProviderRegulationName { get; set; }

    /// <inheritdoc/>
    public int ConsumerTenantId { get; set; }

    /// <inheritdoc/>
    public string ConsumerTenantIdentifier { get; set; }

    /// <inheritdoc/>
    public int? ConsumerDivisionId { get; set; }

    /// <inheritdoc/>
    public string ConsumerDivisionName { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, object> Attributes { get; set; }

    /// <summary>Initializes a new instance</summary>
    public RegulationShare()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public RegulationShare(RegulationShare copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IRegulationShare compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(IRegulationShare compare) =>
        string.Equals(ProviderTenantIdentifier, compare?.ProviderTenantIdentifier) &&
        string.Equals(ProviderRegulationName, compare?.ProviderRegulationName) &&
        string.Equals(ConsumerTenantIdentifier, compare?.ConsumerTenantIdentifier) &&
        string.Equals(ConsumerDivisionName, compare?.ConsumerDivisionName);

        /// <inheritdoc/>
        public override string GetUiString() => 
            $"{ProviderTenantIdentifier} {ProviderRegulationName} > {ConsumerTenantIdentifier}:{ConsumerDivisionName}";

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{GetUiString()} {base.ToString()}";
}