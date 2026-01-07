namespace PayrollEngine.Client.Model;

/// <summary>Extension methods for queries</summary>
public static class ClientQueryExtensions
{
    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="query">The query to append</param>
    /// <param name="uri">The base URI</param>
    /// <returns>The combined result</returns>
    public static string AppendQueryString(this DivisionQuery query, string uri)
    {
        if (query != null)
        {
            uri = QueryExtensions.AppendQueryString(query, uri)
                .AddQueryString(nameof(query.DivisionId), query.DivisionId);
        }
        return uri;
    }

    /// <param name="query">The query to append</param>
    extension(CaseChangeQuery query)
    {
        /// <summary>Build a case change query</summary>
        /// <param name="uri">The base URI</param>
        /// <param name="resultType">The result type</param>
        /// <returns>The combined result</returns>
        public string BuildQueryString(string uri,
            QueryResultType? resultType = null)
        {
            // ensure query
            query ??= new();
            query.Result = resultType;
            return query.AppendQueryString(uri);
        }

        /// <summary>Append the given object query key and value to the URI</summary>
        /// <param name="uri">The base URI</param>
        /// <returns>The combined result</returns>
        public string AppendQueryString(string uri)
        {
            if (query != null)
            {
                uri = QueryExtensions.AppendQueryString(query, uri)
                    .AddQueryString(nameof(query.DivisionId), query.DivisionId)
                    .AddQueryString(nameof(query.Culture), query.Culture)
                    .AddQueryString(nameof(query.ExcludeGlobal), query.ExcludeGlobal);
            }
            return uri;
        }
    }

    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="query">The query to append</param>
    /// <param name="uri">The base URI</param>
    /// <returns>The combined result</returns>
    public static string AppendQueryString(this PayrollCaseChangeQuery query, string uri)
    {
        if (query != null)
        {
            // case change query
            uri = ((CaseChangeQuery)query).AppendQueryString(uri);
            // payroll case change query
            uri = QueryExtensions.AppendQueryString(query, uri)
                .AddQueryString(nameof(query.UserId), query.UserId)
                .AddQueryString(nameof(query.CaseType), query.CaseType)
                .AddQueryString(nameof(query.EmployeeId), query.EmployeeId)
                .AddQueryString(nameof(query.ClusterSetName), query.ClusterSetName)
                .AddQueryString(nameof(query.RegulationDate), query.RegulationDate)
                .AddQueryString(nameof(query.EvaluationDate), query.EvaluationDate)

                .AddQueryString(nameof(query.DivisionId), query.DivisionId)
                .AddQueryString(nameof(query.Culture), query.Culture)
                .AddQueryString(nameof(query.ExcludeGlobal), query.ExcludeGlobal);
        }
        return uri;
    }

    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="query">The query to append</param>
    /// <param name="uri">The base URI</param>
    /// <returns>The combined result</returns>
    public static string AppendQueryString(this CaseValueQuery query, string uri)
    {
        if (query != null)
        {
            uri = QueryExtensions.AppendQueryString(query, uri)
                .AddQueryString(nameof(query.DivisionScope), query.DivisionScope)
                .AddQueryString(nameof(query.DivisionId), query.DivisionId)
                .AddQueryString(nameof(query.CaseName), query.CaseName)
                .AddQueryString(nameof(query.CaseFieldName), query.CaseFieldName);
        }
        return uri;
    }

    /// <summary>Append the given object query key and value to the URI</summary>
    /// <param name="query">The query to append</param>
    /// <param name="uri">The base URI</param>
    /// <returns>The combined result</returns>
    public static string AppendQueryString(this ReportTemplateQuery query, string uri)
    {
        if (query != null && !string.IsNullOrWhiteSpace(query.Culture))
        {
            uri = QueryExtensions.AppendQueryString(query, uri)
                .AddQueryString(nameof(query.Culture), query.Culture);
        }
        return uri;
    }
}