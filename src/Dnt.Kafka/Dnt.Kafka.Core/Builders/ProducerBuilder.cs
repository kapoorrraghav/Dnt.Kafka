using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Dnt.Kafka.Core.Models.EventArgs;
using Dnt.Kafka.Core.Producers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dnt.Kafka.Core.Builders
{
    public class ProducerBuilder<TKey, TValue> : IProducerBuilder<TKey, TValue>
    {
        private readonly string _topic;
        private readonly ISerializer<TKey> _keySerializer;
        private readonly ISerializer<TValue> _valueSerializer;
        private readonly Dictionary<string, object> _properties;
        private EventHandler<Error> _onError;
        private EventHandler<KafkaMessageProducingArgs<TKey, TValue>> _onMessageProducing;
        private EventHandler<KafkaMessageProducedArgs<TKey, TValue>> _onMessageProduced;
        private ITopicProducer<TKey, TValue> _topicProducer;

        public ProducerBuilder(string topic, Dictionary<string, object> properties, ISerializer<TKey> keySerializer, ISerializer<TValue> valueSerializer)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic is required", nameof(topic));
            }

            _topic = topic;

            if (!properties.Any() || !properties.TryGetValue(Constants.KafkaConfiguration.BootstrapServers, out var brokers)
                || brokers == null || brokers.ToString().Split(',').Length == 0)
            {
                throw new InvalidOperationException("Bootstrap servers should be provided as part of properties");
            }

            _properties = properties;

            _keySerializer = keySerializer ?? throw new ArgumentNullException(nameof(keySerializer), "Key serializer cannot be null");
            _valueSerializer = valueSerializer ?? throw new ArgumentNullException(nameof(valueSerializer), "Value serializer cannot be null");
        }

        internal ProducerBuilder(ITopicProducer<TKey, TValue> topicProducer)
        {
            _topicProducer = topicProducer;
        }

        public ITopicProducer<TKey, TValue> Build()
        {
            _topicProducer = _topicProducer ?? new TopicProducer<TKey, TValue>(_topic, _properties, _keySerializer, _valueSerializer);

            AddEvents();

            return _topicProducer;
        }

        public ProducerBuilder<TKey, TValue> OnError(EventHandler<Error> handler)
        {
            _onError = handler;

            return this;
        }

        public ProducerBuilder<TKey, TValue> OnMessageProducing(EventHandler<KafkaMessageProducingArgs<TKey, TValue>> handler)
        {
            _onMessageProducing = handler;

            return this;
        }

        public ProducerBuilder<TKey, TValue> OnMessageProduced(EventHandler<KafkaMessageProducedArgs<TKey, TValue>> handler)
        {
            _onMessageProduced = handler;

            return this;
        }

        private void AddEvents()
        {
            if (_onMessageProducing != null)
            {
                _topicProducer.OnMessageProducingEvent += _onMessageProducing;
            }

            if (_onMessageProduced != null)
            {
                _topicProducer.OnMessageProducedEvent += _onMessageProduced;
            }

            if (_onError != null)
            {
                _topicProducer.OnError += _onError;
            }
        }
    }
}