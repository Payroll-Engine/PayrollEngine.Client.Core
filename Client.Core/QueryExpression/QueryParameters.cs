using System;
using System.Collections.Generic;

namespace PayrollEngine.Client.QueryExpression;

/// <summary>Query parameters</summary>
public class QueryParameters : Dictionary<string, string>
{
    /// <summary>Query filter</summary>
    public QueryParameters Filter(Filter filter) =>
        Parameter(QuerySpecification.FilterOperation, filter?.Expression);

    /// <summary>Query equal id filter</summary>
    public QueryParameters EqualId(int value) =>
        Filter(new EqualId(value));

    /// <summary>Query equal name filter</summary>
    public QueryParameters EqualName(string value) =>
        Filter(new EqualName(value));

    /// <summary>Query equal identifier filter</summary>
    public QueryParameters EqualIdentifier(string value) =>
        Filter(new EqualIdentifier(value));

    /// <summary>Query equals filter</summary>
    public QueryParameters Equals(string field, object value) =>
        Filter(new Equals(field, value));

    /// <summary>Query not equals filter</summary>
    public QueryParameters NotEquals(string field, object value) =>
        Filter(new NotEquals(field, value));

    /// <summary>Query less filter</summary>
    public QueryParameters Less(string field, object value) =>
        Filter(new LessFilter(field, value));

    /// <summary>Query greater filter</summary>
    public QueryParameters Greater(string field, object value) =>
        Filter(new Greater(field, value));

    /// <summary>Query less equals filter</summary>
    public QueryParameters LessEquals(string field, object value) =>
        Filter(new LessEquals(field, value));

    /// <summary>Query greater equals filter</summary>
    public QueryParameters GreaterEquals(string field, object value) =>
        Filter(new GreaterEquals(field, value));

    /// <summary>Query order by</summary>
    public QueryParameters OrderBy(OrderBy orderBy) =>
        Parameter(QuerySpecification.OrderByOperation, orderBy?.Expression);

    /// <summary>Query order by</summary>
    public QueryParameters OrderBy(string field) =>
        OrderBy(new OrderBy(field));

    /// <summary>Query order ascending</summary>
    public QueryParameters OrderAscending(string field) =>
        OrderBy(new OrderByAscending(field));

    /// <summary>Query order descending</summary>
    public QueryParameters OrderDescending(string field) =>
        OrderBy(new OrderByDescending(field));

    /// <summary>Query inactive objects</summary>
    public QueryParameters InactiveStatus() =>
        Status(new Inactive());

    /// <summary>Query active objects</summary>
    public QueryParameters ActiveStatus() =>
        Status(new Active());

    /// <summary>Query object status</summary>
    public QueryParameters Status(Status status) =>
        Parameter(QuerySpecification.StatusOperation, status?.Expression);

    /// <summary>Query select</summary>
    public QueryParameters Select(Select select) =>
        Parameter(QuerySpecification.SelectOperation, select?.Expression);

    /// <summary>Query select</summary>
    public QueryParameters Select(params string[] fields) =>
        Select(new Select(fields));

    /// <summary>Query top</summary>
    public QueryParameters Top(int top) =>
        Parameter(QuerySpecification.TopOperation, top);

    /// <summary>Query skip</summary>
    public QueryParameters Skip(int skip) =>
        Parameter(QuerySpecification.SkipOperation, skip);

    /// <summary>Query id parameter</summary>
    public QueryParameters Id(int id)
    {
        if (id == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        return Parameter("Id", id);
    }

    /// <summary>Query type id parameter</summary>
    public QueryParameters Id<T>(int id) where T : class
    {
        if (id == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        return Parameter($"{typeof(T).Name}Id", id);
    }

    /// <summary>Query name parameter</summary>
    public QueryParameters Name(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }
        return Parameter("Name", name);
    }

    /// <summary>Query identifier parameter</summary>
    public QueryParameters Identifier(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
        {
            throw new ArgumentException(nameof(identifier));
        }
        return Parameter("Identifier", identifier);
    }

    /// <summary>Query parameter</summary>
    public QueryParameters Parameter<TKey, TValue>(KeyValuePair<TKey, TValue> value) =>
        Parameter(new(value.Key.ToString(), value.Value));

    /// <summary>Query parameter</summary>
    public QueryParameters Parameter(string name, object value) =>
        Parameter(new(name, value));

    /// <summary>Query parameter</summary>
    public QueryParameters Parameter(Parameter parameter)
    {
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        if (parameter.Value == null)
        {
            Remove(parameter.Name);
        }
        else
        {
            this[parameter.Name] = parameter.Expression;
        }
        return this;
    }
}