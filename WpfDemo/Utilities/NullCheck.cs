using System;
using System.Linq.Expressions;

namespace WpfDemo.Utilities
{
    public static class Null
    {
        public static void CheckArgument<T>(Expression<Func<T>> memberExpression) where T: class
        {
            var argValue = memberExpression.Compile().Invoke();
            if (argValue == null)
            {
                var expressionBody = (MemberExpression)memberExpression.Body;
                throw new ArgumentNullException(expressionBody.Member.Name);
            }
        }
    }
}
