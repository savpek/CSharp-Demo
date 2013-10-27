using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Components;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class MessageAggregatorUnitTests
    {
        [Test]
        public void RegisterWithSingleConsumer_WorksTogether()
        {
            string message = "Not set";
            var messenger = new MessageAggregator();
            messenger.Subscribe<TestMessage>(m => message = m.ExampleMessage);

            messenger.Publish(new TestMessage {ExampleMessage = "Message example."});

            message.Should().Be("Message example.");
        }

        [Test]
        public void WithMultipleRegisteredTypes_OnlyReactCorrectlyTypedMessage()
        {
            string firstMessage = "Not set";
            string secondMessage = "Not set";
            string doNotTouchMessage = "Should not be changed.";
            var messenger = new MessageAggregator();
            messenger.Subscribe<TestMessage>(m => firstMessage = m.ExampleMessage);
            messenger.Subscribe<TestMessage>(m => secondMessage = m.ExampleMessage);
            messenger.Subscribe<NotUsedTestMessage>(m => doNotTouchMessage = "This should not happen.");

            messenger.Publish(new TestMessage { ExampleMessage = "Message example." });

            firstMessage.Should().Be("Message example.");
            secondMessage.Should().Be("Message example.");
            doNotTouchMessage.Should().Be("Should not be changed.");
        }

        private class TestMessage
        {
            public string ExampleMessage { get; set; } 
        }

        private class NotUsedTestMessage
        {
        }
    }
}
