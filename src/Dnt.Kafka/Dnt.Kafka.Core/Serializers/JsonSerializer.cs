using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Dnt.Kafka.Core.Serializers
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        private readonly StringSerializer _stringSerializer;

        public JsonSerializer()
        {
            _stringSerializer = new StringSerializer(Encoding.UTF8);
        }

        public byte[] Serialize(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"Data for type {typeof(T)} cannot be null");
            }

            var json = JsonConvert.SerializeObject(data);

            return _stringSerializer.Serialize(json);
        }
    }
}
