using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Annotations;
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
            var bar = PropertyChangedInterceptor.Get<TestClass>();
            bar.MonitorEvents();

            bar.FirstProperty = "Foo";

            bar.ShouldRaisePropertyChangeFor(x => x.FirstProperty);
        }

        [Test]
        public void NoPropertyChanged_DontNotifyPropertyChanged()
        {
            var bar = PropertyChangedInterceptor.Get<TestClass>();
            bar.MonitorEvents();

            bar.ShouldNotRaisePropertyChangeFor(x => x.FirstProperty);
        }

        [Test]
        public void PropertyIsNonVirtual_DontNotifyPropertyChanged()
        {
            var bar = PropertyChangedInterceptor.Get<TestClass>();
            bar.MonitorEvents();

            bar.SecondProperty = "Test";

            bar.ShouldNotRaisePropertyChangeFor(x => x.SecondProperty);
        }
    }
}
