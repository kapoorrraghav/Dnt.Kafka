using Confluent.Kafka.Serialization;
using Dnt.Kafka.Core.Builders;
using Dnt.Kafka.Core.Producers;
using Dnt.Kafka.Tests.Core;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dnt.Kafka.Core.Tests.Builders
{
    public class ProducerBuilderTests
    {
        private readonly ISerializer<string> _keySerializer = Substitute.For<ISerializer<string>>();
        private readonly ISerializer<TestMessage> _valueSerializer = Substitute.For<ISerializer<TestMessage>>();

        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>
        {
            {Constants.KafkaConfiguration.BootstrapServers, "localhost:9092"}
        };

        public ProducerBuilder<string, TestMessage> CreateProducerBuilder(string topicName, ISerializer<string> keySerializer, ISerializer<TestMessage> valueSerializer, Dictionary<string, object> properties)
        {
            return new ProducerBuilder<string, TestMessage>(topicName, properties, keySerializer, valueSerializer);
        }

        private ProducerBuilder<string, TestMessage> CreateProducerBuilder()
        {
            return new ProducerBuilder<string, TestMessage>("testTopic", _properties, _keySerializer, _valueSerializer);
        }

        private ITopicProducer<string, TestMessage> CreateTopicProducerSubstitute()
        {
            return Substitute.For<ITopicProducer<string, TestMessage>>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ShouldThrowArgumentExceptionIfTopicIsNotProvided(string topic)
        {
            Assert.Throws<ArgumentException>(() => CreateProducerBuilder(topic, null, null, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfPropertiesArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProducerBuilder<string, TestMessage>("topic", null, null, null));
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionIfPropertiesArgumentHasNoKeyValuePair()
        {
            Assert.Throws<InvalidOperationException>(() => new ProducerBuilder<string, TestMessage>("topic", new Dictionary<string, object>(), null, null));
        }


        [Fact]
        public void ShouldThrowInvalidOperationExceptionIfPropertiesArgumentDoesnotContainBootstrapServers()
        {
            Assert.Throws<InvalidOperationException>(() => new ProducerBuilder<string, TestMessage>("topic", new Dictionary<string, object> {{"test", "test"}}, null, null));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfKeySerializerArgumentIsNotProvided()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ProducerBuilder<string, TestMessage>("topic", _properties, null, null));
            Assert.True(ex.ParamName == "keySerializer");
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfValueSerializerArgumentIsNotProvided()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ProducerBuilder<string, TestMessage>("topic", _properties, Substitute.For<ISerializer<string>>(), null));
            Assert.True(ex.ParamName == "valueSerializer");
        }

        [Fact]
        public void ShouldAttachOnErrorEventOnBuild()
        {
            var topicProducerSubstitute = CreateTopicProducerSubstitute();
            var sut = new ProducerBuilder<string, TestMessage>(topicProducerSubstitute);
            sut.OnError(EventHandlers.OnErrorHandler);
            sut.Build();

            topicProducerSubstitute.Received(1).OnError += EventHandlers.OnErrorHandler;
        }

        [Fact]
        public void ShouldAttachOnMessageProducingOnBuild()
        {
            var topicProducerSubstitute = CreateTopicProducerSubstitute();
            var sut = new ProducerBuilder<string, TestMessage>(topicProducerSubstitute);
            sut.OnMessageProducing(EventHandlers.OnMessageProducing);
            sut.Build();

            topicProducerSubstitute.Received(1).OnMessageProducingEvent += EventHandlers.OnMessageProducing;
        }

        [Fact]
        public void ShouldAttachOnMessageProducedOnBuild()
        {
            var topicProducerSubstitute = CreateTopicProducerSubstitute();
            var sut = new ProducerBuilder<string, TestMessage>(topicProducerSubstitute);
            sut.OnMessageProduced(EventHandlers.OnMessageProduced);
            sut.Build();

            topicProducerSubstitute.Received(1).OnMessageProducedEvent += EventHandlers.OnMessageProduced;
        }
    }
}
