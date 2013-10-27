using Castle.DynamicProxy;

namespace WpfDemo.Components
{
    public class PropertyChangedInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            if (methodName.StartsWith("set_") && methodName != "set_Modified")
            {
                var target = (DataModelBase)invocation.Proxy;
                var propertyName = methodName.Substring(methodName.IndexOf('_') + 1);
                target.OnPropertyChanged(propertyName);
                invocation.Proceed();
            }
            else
            {
                invocation.Proceed();
            }

            invocation.Proceed();
        }

        public static T Get<T>(T target) where T: DataModelBase
        {
            var generator = new ProxyGenerator();
            return generator.CreateClassProxyWithTarget(target, new PropertyChangedInterceptor());
        }
    }
}
