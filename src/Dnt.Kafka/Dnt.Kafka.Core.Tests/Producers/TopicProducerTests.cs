using Confluent.Kafka.Serialization;
using Dnt.Kafka.Core.Models.EventArgs;
using Dnt.Kafka.Core.Producers;
using Dnt.Kafka.Tests.Core;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dnt.Kafka.Core.Tests.Producers
{
    public class TopicProducerTests
    {
        private readonly IKafkaProducer<string, TestMessage> _kafkaProducer;

        public TopicProducerTests()
        {
            _kafkaProducer = Substitute.For<IKafkaProducer<string, TestMessage>>();
        }

        private TopicProducer<string, TestMessage> CreateTopicProducer(string topic)
        {
            return new TopicProducer<string, TestMessage>(topic, _kafkaProducer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ShouldThrowArgumentExceptionIfTopicIsNotProvidedInternalConsutructor(string topic)
        {
            Assert.Throws<ArgumentException>(() => CreateTopicProducer(topic));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ShouldThrowArgumentExceptionIfTopicIsNotProvided(string topic)
        {
            Assert.Throws<ArgumentException>(() => new TopicProducer<string, TestMessage>(topic, null, null, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfPropertiesArgumentIsNotProvided()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TopicProducer<string, TestMessage>("topic", null, null, null));
            Assert.True(ex.ParamName == "properties");
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfKeySerializerArgumentIsNotProvided()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TopicProducer<string, TestMessage>("topic", new Dictionary<string, object>(), null, null));
            Assert.True(ex.ParamName == "keySerializer");
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfValueSerializerArgumentIsNotProvided()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TopicProducer<string, TestMessage>("topic", new Dictionary<string, object>(), Substitute.For<ISerializer<string>>(), null));
            Assert.True(ex.ParamName == "valueSerializer");
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionKafkaProducerProvidedIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TopicProducer<string, TestMessage>("topic", null));
        }

        [Fact]
        public void ShouldCallKafkaProducerProduceAsyncOnceOnProduceAsync()
        {
            var topicName = "testTopic";
            var sut = CreateTopicProducer(topicName);

            sut.ProduceAsync("key1", null);

            _kafkaProducer.Received(1).ProduceAsync(topicName, "key1", null);
        }

        [Fact]
        public void ShouldRaiseOnMessageProducingEventWhenSubscribed()
        {
            var topicName = "testTopic";
            var sut = CreateTopicProducer(topicName);

            var evt = Assert.Raises<KafkaMessageProducingArgs<string, TestMessage>>(
                h => sut.OnMessageProducingEvent += h,
                h => sut.OnMessageProducingEvent -= h,
                () => sut.ProduceAsync("key", null));

            Assert.NotNull(evt);
            Assert.Equal(sut, evt.Sender);
            Assert.IsType<KafkaMessageProducingArgs<string, TestMessage>>(evt.Arguments);
            Assert.Equal(topicName, evt.Arguments.Topic);
            Assert.Equal("key", evt.Arguments.Key);
            Assert.Null(evt.Arguments.Value);
        }

        [Fact]
        public void ShouldAddHandlerToKafkaProducerOnErrorOnAddingHandler()
        {
            var sut = CreateTopicProducer("topicName");

            sut.OnError += EventHandlers.OnErrorHandler;

            _kafkaProducer.Received().OnError += EventHandlers.OnErrorHandler;
        }

        [Fact]
        public void ShouldSetNameAsOnInternalProducer()
        {
            var producerName = "test";
            _kafkaProducer.Name.Returns(producerName);

            var sut = CreateTopicProducer("topicName");

            Assert.Equal(producerName, sut.Name);
        }
    }
}