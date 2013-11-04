using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Components;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class LoggerHelperUnitTests
    {
        [SetUp]
        public void Init()
        {
        }

        private class Dummy
        {
            public string StringProperty { get; set; }
            public int IntProperty { get; set; }
            public object ObjectProperty { get; set; }

            public override string ToString()
            {
                return string.Format("'StringProperty':'{0}', 'IntProperty':'{1}', 'ObjectProperty':'{2}'", StringProperty, IntProperty, ObjectProperty);
            }
        }

        [Test]
        public void WithDefinedProperties_PrintValuesAndNamesCorrectly()
        {

            var dummy = new Dummy {StringProperty = "StringProperty value", IntProperty = 10};
            var results = LoggerHelper.PropertyPrint(dummy, x => x.StringProperty, x => x.IntProperty);

            results.Should().Be("{'StringProperty':'StringProperty value', 'IntProperty':'10', }");
        }

        [Test]
        public void WithNullValues_PrintFieldsAsEmpty()
        {
            var dummy = new Dummy();
            var results = LoggerHelper.PropertyPrint(dummy, x => x.ObjectProperty);

            results.Should().Be("{'ObjectProperty':'', }");
        }

        [Test]
        public void WithVeryLargeAmountOfPrints_BeNearlyAsFastAsToCommonManualToString()
        {
            this.Invoking(x => x.TestExpressionVersion(new Dummy(), 100000)).ExecutionTime().ShouldNotExceed(100.Milliseconds());
            this.Invoking(x => x.TestToStringVersion(new Dummy(), 100000)).ExecutionTime().ShouldNotExceed(100.Milliseconds());
        }

        private void TestExpressionVersion(Dummy targetObject, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var devNull = LoggerHelper.PropertyPrint(targetObject, 
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
