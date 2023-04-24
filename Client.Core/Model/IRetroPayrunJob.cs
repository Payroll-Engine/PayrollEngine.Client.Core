using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The retro payrun job client object</summary>
public interface IRetroPayrunJob : IEquatable<IRetroPayrunJob>
{
    /// <summary>The schedule date</summary>
    DateTime ScheduleDate { get; set; }

    /// <summary>The result tags</summary>
    List<string> ResultTags { get; set; }
}