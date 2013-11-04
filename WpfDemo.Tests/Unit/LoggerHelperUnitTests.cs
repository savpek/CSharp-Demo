using System;
using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Components;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class LoggerHelperUnitTests
    {
        private class Dummy
        {
            public string StringProperty { get; set; }
            public int IntProperty { get; set; }
            public object ObjectProperty { get; set; }

            public object NonPropertyMethod()
            {
                return null;
            }

            public override string ToString()
            {
                return string.Format("Dummy: 'StringProperty':'{0}', 'IntProperty':'{1}', 'ObjectProperty':'{2}'", StringProperty, IntProperty, ObjectProperty);
            }
        }

        [Test]
        public void WithDefinedProperties_PrintValuesAndNamesCorrectly()
        {
            var dummy = new Dummy {StringProperty = "StringProperty value", IntProperty = 10};
            var results = dummy.FormatProperties(x => x.StringProperty, x => x.IntProperty);

            results.Should().Be("{Dummy: 'StringProperty':'StringProperty value', 'IntProperty':'10', }");
        }

        [Test]
        public void WithNullValues_PrintFieldsAsNull()
        {
            var dummy = new Dummy();
            var results = dummy.FormatProperties(x => x.ObjectProperty);

            results.Should().Be("{Dummy: 'ObjectProperty':'null', }");
        }

        [Test]
        public void WithZeroProperties_ThrowNewInvalidOperationException()
        {
            var dummy = new Dummy();
            dummy.Invoking(d => d.FormatProperties())
                .ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void InvokeNonPropertyMethod_ThrowInvalidOperationException()
        {
            var dummy = new Dummy();
            dummy.Invoking(d => d.FormatProperties(x => x.NonPropertyMethod()))
                .ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void WithVeryLargeAmountOfPrints_BeFastEnough()
        {
            this.Invoking(x => x.TestExpressionVersion(new Dummy(), 10000)).ExecutionTime().ShouldNotExceed(300.Milliseconds());
            this.Invoking(x => x.TestToStringVersion(new Dummy(), 10000)).ExecutionTime().ShouldNotExceed(10.Milliseconds());
        }

        private void TestExpressionVersion(Dummy targetObject, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var devNull = targetObject.FormatProperties(
                    x => x.IntProperty, 
                    x => x.ObjectProperty,
                    x => x.StringProperty);
            }
        }

        private void TestToStringVersion(Dummy targetObject, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var devNull = targetObject.ToString();
            }
        }
    }
}
