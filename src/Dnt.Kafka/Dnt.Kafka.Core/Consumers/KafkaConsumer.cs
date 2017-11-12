using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Collections.Generic;

namespace Dnt.Kafka.Core.Consumers
{
    public class KafkaConsumer<TKey,TValue> : Consumer<TKey, TValue>, IKafkaConsumer<TKey, TValue>
    {
        public KafkaConsumer(IEnumerable<KeyValuePair<string, object>> config, IDeserializer<TKey> keyDeserializer, IDeserializer<TValue> valueDeserializer) : base(config, keyDeserializer, valueDeserializer)
        {
        }
    }
}
