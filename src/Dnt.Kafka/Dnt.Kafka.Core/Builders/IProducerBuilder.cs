using Dnt.Kafka.Core.Producers;

namespace Dnt.Kafka.Core.Builders
{
    public interface IProducerBuilder<TKey, TValue>
    {
        ITopicProducer<TKey, TValue> Build();
    }
}