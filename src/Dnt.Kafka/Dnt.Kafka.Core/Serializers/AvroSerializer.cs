using Confluent.Kafka.Serialization;
using System;

namespace Dnt.Kafka.Core.Serializers
{
    public class AvroSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data)
        {
            throw new NotImplementedException();
        }
    }
}
