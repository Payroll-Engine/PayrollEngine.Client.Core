
using System.Text.Json.Serialization;

namespace PayrollEngine.Client.Model;

/// <summary>The payrun job client object</summary>
public class PayrunJobEmployee : ModelBase, IPayrunJobEmployee
{
    /// <summary>The employee id (immutable)</summary>
    [JsonPropertyOrder(100)]
    public int EmployeeId { get; set; }

    /// <summary>Initializes a new instance</summary>
    public PayrunJobEmployee()
    {
    }

    /// <summary>Initializes a new instance from a copy</summary>
    /// <param name="copySource">The copy source</param>
    public PayrunJobEmployee(PayrunJobEmployee copySource) :
        base(copySource)
    {
        CopyTool.CopyProperties(copySource, this);
    }

    /// <inheritdoc/>
    public virtual bool Equals(IPayrunJobEmployee compare) =>
        CompareTool.EqualProperties(this, compare);

    /// <inheritdoc/>
    public override string GetUiString() => EmployeeId.ToString();
}