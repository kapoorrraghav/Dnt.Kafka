using Confluent.Kafka.Serialization;
using Microsoft.Hadoop.Avro;
using System;
using System.IO;

namespace Dnt.Kafka.Core.Serializers
{
    public class AvroSerializer<T> : ISerializer<T>
    {
        private readonly IAvroSerializer<T> _avroSerializer;

        public AvroSerializer()
        {
            _avroSerializer = AvroSerializer.Create<T>();
        }

        public byte[] Serialize(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"Data for type {typeof(T)} cannot be null");
            }

            using (var ms = new MemoryStream())
            {
                _avroSerializer.Serialize(ms, data);

                return ms.ToArray();
            }
        }
    }
}
