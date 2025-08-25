using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using PayrollEngine.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PayrollEngine.Client.Model;

/// <summary>Represents a value within a lookup</summary>
public class LookupValue : ModelBase, ILookupValue
{
    /// <inheritdoc/>
    [JsonPropertyOrder(100)]
    public string Key { get; set; }

    private object[] keyValues;

    /// <summary>The lookup key values (client only)</summary>
    [JsonPropertyOrder(101)]
    public object[] KeyValues
    {
        get => keyValues;
        set
        {
            keyValues = value;
            if (value != null && value.Any())
            {
                Key = JsonSerializer.Serialize(value);
            }
        }
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(102)]
    public string Value { get; set; }

    /// <summary>The lookup value object</summary>
    [JsonPropertyOrder(103)]
    public object ValueObject
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return null;
            }
            if (Value.IsJsonArray())
            {
                return ClientJsonSerializer.Deserialize<object[]>(Value);
            }
            if (Value.IsJsonObject())
            {
                return ClientJsonSerializer.Deserialize<object>(Value);
            }
            return Value;
        }
        set => Value = value == null ? null : ClientJsonSerializer.Serialize(value);
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(104)]
    public Dictionary<string, string> ValueLocalizations { get; set; }

    /// <summary>The localized lookup values</summary>
    [JsonPropertyOrder(100)]
    public Dictionary<string, object> ValueObjectLocalizations
    {
        get => ValueLocalizations?.ToDictionary(x => x.Key, x => (object)x.Value);
        set
        {
            if (value == null || !value.Any())
            {
                ValueLocalizations = null;
                return;
            }
            var objects = new Dictionary<string, string>();
            foreach (var valueLocalization in value)
            {
                objects.Add(valueLocalization.Key, DefaultJsonSerializer.Serialize(valueLocalization.Value));
            }
            ValueLocalizations = objects;
        }
    }

    /// <inheritdoc/>
    [JsonPropertyOrder(105)]
    public decimal? RangeValue { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(106)]
    public OverrideType OverrideType { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupValue()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public LookupValue(LookupValue copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(ILookupValue compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public virtual bool EqualKey(ILookupValue compare) =>
        string.Equals(Key, compare?.Key);

    /// <inheritdoc/>
    public override string GetUiString() => Key;
}