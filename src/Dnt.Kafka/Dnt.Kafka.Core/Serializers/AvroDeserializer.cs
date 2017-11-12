using System;
using System.IO;
using Confluent.Kafka.Serialization;
using Microsoft.Hadoop.Avro;

namespace Dnt.Kafka.Core.Serializers
{
    public class AvroDeserializer<T> : IDeserializer<T>
    {
        private readonly IAvroSerializer<T> _avroSerializer;

        public AvroDeserializer()
        {
            _avroSerializer = AvroSerializer.Create<T>();
        }

        public T Deserialize(byte[] data)
        {
            if (data == default(byte[]))
            {
                throw new ArgumentNullException(nameof(data), $"Data for type {typeof(T)} cannot be null");
            }

            var ms = new MemoryStream(data);
            var retVal = _avroSerializer.Deserialize(ms);

            return retVal;
        }
    }
}
