using Confluent.Kafka;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Dnt.Kafka.Core.Tests")]
namespace Dnt.Kafka.Core.Producers
{
    public interface IKafkaProducer<TKey, TValue> : IDisposable
    {
        int Flush(int milliSeconds);

        Task<Message<TKey, TValue>> ProduceAsync(string topicName, TKey key, TValue value);

        event EventHandler<Error> OnError;

        string Name { get; }
    }
}