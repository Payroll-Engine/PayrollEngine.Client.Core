using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The exchange object</summary>
public class Exchange : IExchange
{
    /// <summary>
    /// Json schema
    /// </summary>
    [JsonPropertyName("$schema")]
    [JsonPropertyOrder(1)]
    public string Schema { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(2)]
    public DateTime? CreatedObjectDate { get; set; }

    /// <inheritdoc/>
    [Required]
    [JsonPropertyOrder(3)]
    public List<ExchangeTenant> Tenants { get; set; }

    /// <inheritdoc/>
    [JsonPropertyOrder(4)]
    public List<RegulationShare> RegulationShares { get; set; }

    /// <summary>Initializes a new instance</summary>
    public Exchange()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public Exchange(Exchange copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual void Import(IExchange source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        // tenants
        if (source.Tenants != null)
        {
            Tenants ??= [];
            foreach (var sourceTenant in source.Tenants)
            {
                var tenant = Tenants.FirstOrDefault(x => string.Equals(x.Identifier, sourceTenant.Identifier));
                if (tenant != null)
                {
                    // merge
                    tenant.Import(sourceTenant);
                }
                else
                {
                    // new
                    Tenants.Add(sourceTenant);
                }
            }
        }

        // regulation shares
        if (source.RegulationShares != null)
        {
            RegulationShares ??= [];
            RegulationShares.AddRange(source.RegulationShares);
        }

        // created object date
        if (!CreatedObjectDate.HasValue && source.CreatedObjectDate.HasValue)
        {
            CreatedObjectDate = source.CreatedObjectDate;
        }
    }

    /// <inheritdoc/>
    public virtual bool Equals(IExchange compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <summary>Returns a <see cref="string" /> that represents this instance</summary>
    /// <returns>A <see cref="string" /> that represents this instance</returns>
    public override string ToString()
    {
        if (Tenants == null || Tenants.Count == 0)
        {
            return base.ToString();
        }
        return Tenants.Count == 1 ?
            $"{Tenants.Count} tenant {base.ToString()}" :
            $"{Tenants.Count} tenants {base.ToString()}";
    }
}