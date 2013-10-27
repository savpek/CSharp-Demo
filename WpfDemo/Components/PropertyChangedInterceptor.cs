using Castle.DynamicProxy;

namespace WpfDemo.Components
{
    public class PropertyChangedInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;
            object target = invocation.InvocationTarget;

            if (methodName.StartsWith("set_") && methodName != "set_Modified")
            {
                var obj = (DataModelBase)target;
                var propertyName = methodName.Substring(methodName.IndexOf('_') + 1);
                obj.OnPropertyChanged(propertyName);
                invocation.Proceed();
            }
            else
            {
                invocation.Proceed();
            }

            invocation.Proceed();
        }

        public static T Get<T>() where T: DataModelBase
        {
            var generator = new ProxyGenerator();

            var t = generator.CreateClassProxy<T>(new PropertyChangedInterceptor());

            return t;
        }
    }
}
