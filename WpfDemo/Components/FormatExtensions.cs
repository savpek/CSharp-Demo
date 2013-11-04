using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WpfDemo.Components
{
    public static class FormatExtensions
    {
        // Reflection is slow, this cache gives ~200 times faster execution times.
        private static readonly ConcurrentDictionary<string, object> ExpressionCache = new ConcurrentDictionary<string, object>();

        public static string FormatProperties<T>(this T targetObject, params Expression<Func<T, object>>[] expressions)
        {
            if(expressions.Length == 0)
                throw new InvalidOperationException("Tried to print class with properties formatting, but no properties defined!");

            var results = "{" + typeof(T).Name + ": ";
            const string template = "'{0}':'{1}', ";

            foreach (var expression in expressions)
            {
                results += string.Format(template, GetExpressionName(expression), GetExpressionValue(expression, targetObject));
            }

            return results + "}";
        }

        private static object GetExpressionValue<T>(Expression<Func<T, object>> expression, T targetObject)
        {
            var key = typeof(T) + expression.ToString();

            if (ExpressionCache.ContainsKey(key))
                return ((Func<T, object>)ExpressionCache[key]).Invoke(targetObject);

            var compiled = expression.Compile();
            ExpressionCache.TryAdd(key, compiled);

            return compiled.Invoke(targetObject) ?? "null";
        }

        private static string GetExpressionName<T>(Expression<Func<T, object>> expression)
        {
            var memberBody = expression.Body as MemberExpression;
            if (memberBody != null)
                return memberBody.Member.Name;

            var unaryBody = expression.Body as UnaryExpression;
            if (unaryBody != null)
            {
                var operand = unaryBody.Operand;
                return ((MemberExpression)operand).Member.Name;    
            }

            throw new InvalidOperationException(
                string.Format("Tried to format property from object, but defined field is not property. Invalid expression: '{0}'", expression));
        }
    }
}
