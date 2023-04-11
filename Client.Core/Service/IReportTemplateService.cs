using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Regulation report template service</summary>
public interface IReportTemplateService : ICrudService<IReportTemplate, ReportServiceContext, ReportTemplateQuery>
{
}