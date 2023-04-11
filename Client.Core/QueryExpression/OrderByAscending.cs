
namespace PayrollEngine.Client.QueryExpression
{
    /// <summary>Query order by ascending</summary>
    public class OrderByAscending : OrderBy
    {
        /// <summary>Constructor</summary>
        /// <param name="field">The order field</param>
        public OrderByAscending(string field) :
            base(field, OrderDirection.Ascending)
        {
        }
    }
}
