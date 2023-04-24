using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The case value setup (immutable)</summary>
public interface ICaseValueSetup : ICaseValue, IEquatable<ICaseValueSetup>
{
    /// <summary>Case documents</summary>
    List<CaseDocument> Documents { get; set; }
}