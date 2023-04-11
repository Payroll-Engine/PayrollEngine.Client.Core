
namespace PayrollEngine.Client.QueryExpression
{
    /// <summary>Query order by descending</summary>
    public class OrderByDescending : OrderBy
    {
        /// <summary>Constructor</summary>
        /// <param name="field">The order field</param>
        public OrderByDescending(string field) :
            base(field, OrderDirection.Descending)
        {
        }
    }
}
