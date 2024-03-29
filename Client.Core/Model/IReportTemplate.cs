﻿using System;

namespace PayrollEngine.Client.Model;

/// <summary>The report template client object</summary>
public interface IReportTemplate : IModel, IAttributeObject, IEquatable<IReportTemplate>
{
    /// <summary>The payroll report template name</summary>
    public string Name { get; set; }

    /// <summary>The report culture</summary>
    string Culture { get; set; }

    /// <summary>The report content (client owned)</summary>
    string Content { get; set; }

    /// <summary>The report content file name (client only)</summary>
    string ContentFile { get; set; }

    /// <summary>The report content type</summary>
    string ContentType { get; set; }

    /// <summary>The report schema (client owned)</summary>
    string Schema { get; set; }

    /// <summary>The report schema file name (client only)</summary>
    string SchemaFile { get; set; }

    /// <summary>The report external resource</summary>
    string Resource { get; set; }

    /// <summary>The override type</summary>
    OverrideType OverrideType { get; set; }
}