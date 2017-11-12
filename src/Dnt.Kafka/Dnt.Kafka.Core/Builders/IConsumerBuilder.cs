using Dnt.Kafka.Core.Consumers;

namespace Dnt.Kafka.Core.Builders
{
    public interface IConsumerBuilder<TKey, TValue>
    {
        ITopicConsumer<TKey, TValue> Build();
    }
}