using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll webhook message service</summary>
public interface IWebhookMessageService : ICrudService<IWebhookMessage, WebhookServiceContext, Query>
{
}