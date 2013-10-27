using System;
using FluentAssertions;
using NUnit.Core;
using NUnit.Framework;
using WpfDemo.Components;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class NullUnitTests
    {
        private void TestMethod(object argument)
        {
            Null.CheckArgument(() => argument);
        }

        [Test]
        public void IfArgumentIsNull_ThrowException()
        {
            this.Invoking(x => x.TestMethod(null))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("Value cannot be null.\r\nParameter name: argument");
        }

        [Test]
        public void IfArgumentIsNotNull_DontThrowException()
        {
            this.Invoking(x => x.TestMethod(new object()))
                .ShouldNotThrow<ArgumentNullException>();
        }
    }
}
