using Dnt.Kafka.Core.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

[assembly:InternalsVisibleTo("Dnt.Kafka.Core.Tests")]
namespace Dnt.Kafka.Core.Producers
{
    public class TopicProducer<TKey, TValue> : ITopicProducer<TKey, TValue>
    {
        private string _topic;
        public event EventHandler<KafkaMessageProducingArgs<TKey, TValue>> OnMessageProducingEvent;
        public event EventHandler<KafkaMessageProducedArgs<TKey, TValue>> OnMessageProducedEvent;
        private IKafkaProducer<TKey, TValue> _producer;

        private void Setup(string topic, IKafkaProducer<TKey, TValue> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic is required", nameof(topic));
            }

            _topic = topic;
        }

        internal TopicProducer(string topic, IKafkaProducer<TKey, TValue> producer)
        {
            Setup(topic, producer);
        }

        public TopicProducer(string topic, IEnumerable<KeyValuePair<string, object>> properties, ISerializer<TKey> keySerializer,
            ISerializer<TValue> valueSerializer)
        {
            if(string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic is required", nameof(topic));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if(keySerializer == null)
                throw new ArgumentNullException(nameof(keySerializer));

            if(valueSerializer == null)
                throw new ArgumentNullException(nameof(valueSerializer));

            var kafkaProducer = new KafkaProducer<TKey,TValue>(properties, keySerializer, valueSerializer);

            Setup(topic, kafkaProducer);
        }

        public void ProduceAsync(TKey key, TValue value)
        {
            ProduceAsync(_topic, key, value);
        }

        private void ProduceAsync(string topic, TKey key, TValue value)
        {
            OnMessageProducingEvent?.Invoke(this, new KafkaMessageProducingArgs<TKey, TValue>(topic, key, value));

            var deliveryReport = _producer.ProduceAsync(topic, key, value);
            deliveryReport.ContinueWith(x =>
            {
                OnMessageProducedEvent?.Invoke(this, new KafkaMessageProducedArgs<TKey, TValue>(key, value, x.Result));
            });
        }

        public event EventHandler<Error> OnError
        {
            add => _producer.OnError += value;
            remove => _producer.OnError -= value;
        }

        public string Name => _producer?.Name;


        public void Dispose()
        {
            if (_producer == null)
                return;

            _producer.Flush(10000);
            _producer.Dispose();
        }
    }
}
