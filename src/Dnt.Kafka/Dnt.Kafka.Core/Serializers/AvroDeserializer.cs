using Confluent.Kafka.Serialization;

namespace Dnt.Kafka.Core.Serializers
{
    public class AvroDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}
