using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Dnt.Kafka.Core.Tests")]
namespace Dnt.Kafka.Core.Producers
{
    public class KafkaProducer<TKey, TValue> : Producer<TKey,TValue>, IKafkaProducer<TKey, TValue>
    {
        public KafkaProducer(IEnumerable<KeyValuePair<string, object>> config, ISerializer<TKey> keySerializer, ISerializer<TValue> valueSerializer) : base(config, keySerializer, valueSerializer)
        {
        }
    }
}
