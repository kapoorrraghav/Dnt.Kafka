using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Dnt.Kafka.Core.Serializers
{
    public class JsonDeserializer<T> : IDeserializer<T>
    {
        private readonly StringDeserializer _stringDeserializer;

        public JsonDeserializer()
        {
            _stringDeserializer = new StringDeserializer(Encoding.UTF8);
        }

        public T Deserialize(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"Data for type {typeof(T)} cannot be null");
            }

            var json = _stringDeserializer.Deserialize(data);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
