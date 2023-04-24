using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>The payroll regulation client object</summary>
public interface IRegulation : IModel, IAttributeObject, IKeyEquatable<IRegulation>
{
    /// <summary>The regulation name</summary>
    string Name { get; set; }

    /// <summary>The localized regulation names</summary>
    Dictionary<string, string> NameLocalizations { get; set; }

    /// <summary>The regulation version, unique per regulation name</summary>
    int Version { get; set; }

    /// <summary>Shared regulation (immutable)</summary>
    bool SharedRegulation { get; set; }

    /// <summary>The date the regulation to be in force, anytime if undefined</summary>
    DateTime? ValidFrom { get; set; }

    /// <summary>The owner name</summary>
    string Owner { get; set; }

    /// <summary>The regulation description</summary>
    string Description { get; set; }

    /// <summary>The localized payroll descriptions</summary>
    Dictionary<string, string> DescriptionLocalizations { get; set; }

    /// <summary>Required base regulations</summary>
    List<string> BaseRegulations { get; set; }
}