
namespace PayrollEngine.Client.QueryExpression
{
    /// <summary>Inactive status expression</summary>
    public class Inactive : Status
    {
        /// <summary>Constructor</summary>
        public Inactive() :
            base(ObjectStatus.Inactive)
        {
        }
    }
}
