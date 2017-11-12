using System;
using Confluent.Kafka;
using Dnt.Kafka.Core.Models.EventArgs;

namespace Dnt.Kafka.Core.Producers
{
    public interface ITopicProducer<TKey, TValue> : IDisposable
    {
        void ProduceAsync(TKey key, TValue value);

        event EventHandler<KafkaMessageProducingArgs<TKey, TValue>> OnMessageProducingEvent;
        event EventHandler<KafkaMessageProducedArgs<TKey, TValue>> OnMessageProducedEvent;

        event EventHandler<Error> OnError;

        string Name { get; }
    }
}