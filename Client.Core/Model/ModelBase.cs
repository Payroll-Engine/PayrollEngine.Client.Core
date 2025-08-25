﻿using System;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>Base for all Payroll models</summary>
/// <remarks>JSON property order range from 0 to 99</remarks>
public abstract class ModelBase : IModel
{
    /// <inheritdoc/>
    [JsonPropertyOrder(1)]
    public int Id { get; set; }

    /// <inheritdoc/>
    public bool IsExistingObject => Id != 0;

    /// <inheritdoc/>
    public bool IsNewObject => !IsExistingObject;

    /// <inheritdoc/>
    [JsonPropertyOrder(2)]
    public ObjectStatus Status { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(3)]
    public DateTime Created { get; set; } = Date.MinValue;

    /// <inheritdoc/>
    [JsonPropertyOrder(4)]
    public DateTime Updated { get; set; } = Date.MinValue;

    /// <inheritdoc/>
    [JsonPropertyOrder(5)]
    public UpdateMode UpdateMode { get; set; }

    /// <summary>Initializes a new instance</summary>
    protected ModelBase()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    protected ModelBase(ModelBase copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public abstract string GetUiString();

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString()
    {
        var uiString = GetUiString();
        return string.IsNullOrWhiteSpace(uiString) ? $"[#{Id}]" : $"{uiString} [#{Id}]";
    }
}