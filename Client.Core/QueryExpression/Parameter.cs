using System;
using System.Text.Json;

namespace PayrollEngine.Client.QueryExpression
{
    /// <summary>Query parameter</summary>
    public class Parameter : ExpressionBase
    {
        /// <summary>The parameter name</summary>
        public string Name { get; }

        /// <summary>The parameter value</summary>
        public object Value { get; }

        /// <summary>Constructor</summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        public Parameter(string name, object value) :
            base(GetValueExpression(value))
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }
            Name = name;
            Value = value;
        }

        private static string GetValueExpression(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.GetType().IsArray ?
                    JsonSerializer.Serialize(value) : value.ToString();
        }
    }
}
