
namespace PayrollEngine.Client.Model;

/// <summary>The regulation permission client object</summary>
public interface IRegulationPermission : IModel, IAttributeObject
{
    /// <summary>The tenant id</summary>
    int TenantId { get; set; }

    /// <summary>The tenant identifier</summary>
    string TenantIdentifier { get; set; }

    /// <summary>The regulation id</summary>
    int RegulationId { get; set; }

    /// <summary>The regulation name</summary>
    string RegulationName { get; set; }

    /// <summary>The permission tenant id</summary>
    int PermissionTenantId { get; set; }

    /// <summary>The permission tenant identifier</summary>
    string PermissionTenantIdentifier { get; set; }

    /// <summary>The permission division id</summary>
    int? PermissionDivisionId { get; set; }

    /// <summary>The permission division name</summary>
    string PermissionDivisionName { get; set; }
}