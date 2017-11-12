using Dnt.Kafka.Core.Consumers;

namespace Dnt.Kafka.Core.Builders
{
    public class ConsumerBuilder<TKey, TValue> : IConsumerBuilder<TKey, TValue>
    {
        public ITopicConsumer<TKey, TValue> Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
