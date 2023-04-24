using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll task object</summary>
public interface ITask : IModel, IAttributeObject, IEquatable<ITask>
{
    /// <summary>The task name (immutable)</summary>
    string Name { get; set; }

    /// <summary>The localized task names (immutable)</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The task category</summary>
    string Category { get; set; }

    /// <summary>The task instruction</summary>
    string Instruction { get; set; }

    /// <summary>The scheduled user id</summary>
    int ScheduledUserId { get; set; }

    /// <summary>The scheduled user identifier</summary>
    string ScheduledUserIdentifier { get; set; }

    /// <summary>The task schedule date (immutable)</summary>
    DateTime Scheduled { get; set; }

    /// <summary>The completed user id</summary>
    int? CompletedUserId { get; set; }

    /// <summary>The completed user identifier</summary>
    string CompletedUserIdentifier { get; set; }

    /// <summary>The task completed date</summary>
    DateTime? Completed { get; set; }
}