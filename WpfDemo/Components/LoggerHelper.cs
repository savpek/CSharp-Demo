using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WpfDemo.Components
{
    public class LoggerHelper
    {
        private static readonly Dictionary<string, object> ExpressionCache = new Dictionary<string, object>();

        public static string PropertyPrint<T>(T targetObject, params Expression<Func<T, object>>[] expressions)
        {
            var results = "{";
            const string template = "'{0}':'{1}', ";

            foreach (var expression in expressions)
            {
                results += string.Format(template, GetExpressionName(expression), GetExpressionValue(expression, targetObject));
            }

            return results + "}";
        }

        private static object GetExpressionValue<T>(Expression<Func<T, object>> expression, T targetObject)
        {
            var key = expression.ToString() + typeof(T);
            if (ExpressionCache.ContainsKey(key))
                return ((Func<T, object>)ExpressionCache[key]).Invoke(targetObject);

            var compiled = expression.Compile();
            ExpressionCache.Add(key, compiled);

            return compiled.Invoke(targetObject);
        }

        private static string GetExpressionName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression)expression.Body).Member.Name;
            }
            
            var op = ((UnaryExpression)expression.Body).Operand;
            return ((MemberExpression)op).Member.Name;
        }
    }
}
