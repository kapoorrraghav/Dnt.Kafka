using Confluent.Kafka;

namespace Test.Abc
{
    public class Test { }
}

namespace Dnt.Kafka.Core.Models.EventArgs
{
    public class KafkaMessageProducingArgs<TKey, TValue> : System.EventArgs
    {
        public KafkaMessageProducingArgs(string topic, TKey key, TValue value)
        {
            Topic = topic;
            Key = key;
            Value = value;
        }

        public string Topic { get; set; }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }

    public class KafkaMessageProducedArgs<TKey, TValue> : System.EventArgs
    {
        public KafkaMessageProducedArgs(TKey key, TValue value, Message<TKey, TValue> message)
        {
            Key = key;
            Value = value;
            Message = message;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public Message<TKey, TValue> Message { get; set; }
    }
}
