using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Shared regulation service</summary>
public interface ISharedRegulationService : ICrudService<IRegulationPermission, RootServiceContext, Query>, IAttributeService<RootServiceContext>
{
}