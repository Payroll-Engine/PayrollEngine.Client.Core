using System;
using System.Collections.Generic;
using PayrollEngine.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace PayrollEngine.Client.Exchange;

/// <summary>
/// Property filter inspector
/// </summary>
public class PropertyFilteringInspector : TypeInspectorSkeleton
{
    private readonly ITypeInspector inner;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="inner">Inner inspector</param>
    public PropertyFilteringInspector(ITypeInspector inner)
    {
        this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    /// <summary>
    /// Get available properties
    /// </summary>
    /// <param name="type">Object type</param>
    /// <param name="container">Object container</param>
    public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
    {
        var properties = new List<IPropertyDescriptor>();
        foreach (var property in inner.GetProperties(type, container))
        {
            // read only
            if (property.GetCustomAttribute<JsonReadOnlyAttribute>() == null)
            {
                properties.Add(property);
            }
        }
        return properties;
    }

    /// <summary>
    /// Get enum value string
    /// </summary>
    /// <param name="enumValue">Enum value</param>
    public override string GetEnumValue(object enumValue) =>
        inner.GetEnumValue(enumValue);

    /// <summary>
    /// Get enum name
    /// </summary>
    /// <param name="enumType">Enum type</param>
    /// <param name="name">Enum name</param>
    public override string GetEnumName(Type enumType, string name) =>
        inner.GetEnumName(enumType, name);
}
