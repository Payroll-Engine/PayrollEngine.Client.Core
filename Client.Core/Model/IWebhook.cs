namespace PayrollEngine.Client.Model;

/// <summary>The Webhook client object</summary>
public interface IWebhook : IModel, IAttributeObject, IKeyEquatable<IWebhook>
{
    /// <summary>The payroll name</summary>
    string Name { get; set; }

    /// <summary>The webhook receiver address</summary>
    string ReceiverAddress { get; set; }

    /// <summary>The web hook trigger action</summary>
    WebhookAction Action { get; set; }
}