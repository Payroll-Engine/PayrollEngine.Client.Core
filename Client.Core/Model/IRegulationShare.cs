namespace PayrollEngine.Client.Model;

/// <summary>The regulation share client object</summary>
public interface IRegulationShare : IModel, IAttributeObject, IKeyEquatable<IRegulationShare>
{
    /// <summary>The provider tenant id</summary>
    int ProviderTenantId { get; set; }

    /// <summary>The provider tenant identifier (client only)</summary>
    string ProviderTenantIdentifier { get; set; }

    /// <summary>The provider regulation id</summary>
    int ProviderRegulationId { get; set; }

    /// <summary>The provider regulation name (client only)</summary>
    string ProviderRegulationName { get; set; }

    /// <summary>The consumer tenant id</summary>
    int ConsumerTenantId { get; set; }

    /// <summary>The consumer tenant identifier (client only)</summary>
    string ConsumerTenantIdentifier { get; set; }

    /// <summary>The consumer division id</summary>
    int? ConsumerDivisionId { get; set; }

    /// <summary>The consumer division name (client only)</summary>
    string ConsumerDivisionName { get; set; }
}