using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation share service</summary>
public interface IRegulationShareService : ICrudService<IRegulationShare, RootServiceContext, Query>, IAttributeService<RootServiceContext>
{
}