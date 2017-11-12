using Confluent.Kafka;
using Dnt.Kafka.Core.Models.EventArgs;
using Dnt.Kafka.Tests.Core;

namespace Dnt.Kafka.Core.Tests
{
    public static class EventHandlers
    {
        public static void OnErrorHandler(object sender, Error error)
        {
        }

        public static void OnMessageProducing(object sender, KafkaMessageProducingArgs<string,TestMessage> error)
        {
        }

        public static void OnMessageProduced(object sender, KafkaMessageProducedArgs<string, TestMessage> error)
        {
        }
    }
}
