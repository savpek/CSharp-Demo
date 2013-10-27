using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Components;

namespace WpfDemo.Tests.Unit
{
    public class TestClass : DataModelBase
    {
        public virtual string FirstProperty { get; set; }
        public string SecondProperty { get; set; }
    }

    [TestFixture]
    public class PropertyChangedInterceptorUnitTests
    {
        [Test]
        public void PropertyChanged_NotifyPropertyChanged()
        {
            var targetObject = new TestClass();
            var intercepted = PropertyChangedInterceptor.Get(targetObject);
            intercepted.MonitorEvents();

            intercepted.FirstProperty = "Foo";

            intercepted.ShouldRaisePropertyChangeFor(x => x.FirstProperty);
        }

        [Test]
        public void NoPropertyChanged_DontNotifyPropertyChanged()
        {
            var targetObject = new TestClass();
            var intercepted = PropertyChangedInterceptor.Get(targetObject);
            intercepted.MonitorEvents();

            intercepted.ShouldNotRaisePropertyChangeFor(x => x.FirstProperty);
        }

        [Test]
        public void PropertyIsNonVirtual_DontNotifyPropertyChanged()
        {
            var targetObject = new TestClass();
            var intercepted = PropertyChangedInterceptor.Get(targetObject);
            intercepted.MonitorEvents();

            intercepted.SecondProperty = "Test";

            intercepted.ShouldNotRaisePropertyChangeFor(x => x.SecondProperty);
        }
    }
}
