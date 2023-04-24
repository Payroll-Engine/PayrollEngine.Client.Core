using System.Collections.Generic;
using System.Linq;
using PayrollEngine.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PayrollEngine.Client.Model;

/// <summary>Represents a value within a lookup</summary>
public class LookupValue : Model, ILookupValue
{
    /// <inheritdoc/>
    public string Key { get; set; }

    private object[] keyValues;
    /// <summary>The lookup key values (client only)</summary>
    public object[] KeyValues
    {
        get => keyValues;
        set
        {
            keyValues = value;
            Key = value == null || !value.Any() ? null : JsonSerializer.Serialize(value);
        }
    }

    /// <inheritdoc/>
    public string Value { get; set; }

    /// <summary>The lookup value object</summary>
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
    public Dictionary<string, string> ValueLocalizations { get; set; }

    /// <summary>The localized lookup values</summary>
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
    public decimal? RangeValue { get; set; }

    /// <summary>Initializes a new instance</summary>
    public LookupValue()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public LookupValue(ILookupValue copySource) :
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

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString() =>
        $"{Key} {base.ToString()}";
}