using System;
using System.Linq.Expressions;

namespace WpfDemo.Components
{
    public class LoggerHelper
    {
        public static string PropertyPrint<T>(T targetObject, params Expression<Func<T, object>>[] expressions)
        {
            var results = "{";
            const string template = "'{0}':'{1}', ";

            foreach (var expression in expressions)
            {
                var argValue = expression.Compile().Invoke(targetObject);
                results += string.Format(template, GetExpressionName(expression), argValue);
            }

            return results + "}";
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
