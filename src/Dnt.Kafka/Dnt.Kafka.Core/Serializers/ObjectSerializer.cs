using Confluent.Kafka.Serialization;

namespace Dnt.Kafka.Core.Serializers
{
    public class ObjectSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data)
        {
            throw new System.NotImplementedException();
        }
    }
}
