using System;

namespace PayrollEngine.Client.Model;

/// <summary>The Webhook message client object</summary>
public interface IWebhookMessage : IModel, IEquatable<IWebhookMessage>
{
    /// <summary>The webhook action name</summary>
    string ActionName { get; set; }

    /// <summary>The webhook receiver address</summary>
    string ReceiverAddress { get; set; }

    /// <summary>The request date</summary>
    DateTime RequestDate { get; set; }

    /// <summary>The request message</summary>
    string RequestMessage { get; set; }

    /// <summary>The request operation</summary>
    string RequestOperation { get; set; }

    /// <summary>The response date</summary>
    DateTime ResponseDate { get; set; }

    /// <summary>The response HTTP status code</summary>
    int ResponseStatus { get; set; }

    /// <summary>The response message</summary>
    string ResponseMessage { get; set; }
}