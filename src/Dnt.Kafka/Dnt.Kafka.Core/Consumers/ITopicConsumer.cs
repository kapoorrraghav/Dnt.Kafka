using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dnt.Kafka.Core.Consumers
{
    public interface ITopicConsumer<TKey, TValue> : IDisposable
    {
        void Poll();

        void Assign(IEnumerable<TopicPartitionOffset> partitionsWithOffset);

        void Assign(IEnumerable<TopicPartition> partitions);

        void Unassign();

        void Subscribe(string topic);

        Task<CommittedOffsets> CommitAsync(Message<TKey, TValue> message);

        List<string> Subscription { get; }

        List<TopicPartition> Assignment { get; }


        event EventHandler<Message<TKey, TValue>> OnMessage;
        event EventHandler<TopicPartitionOffset> OnPartitionEof;
        event EventHandler<Message> OnConsumeError;
        event EventHandler<Error> OnError;
        event EventHandler<CommittedOffsets> OnOffsetsCommitted;
        event EventHandler<List<TopicPartition>> OnPartitionsAssigned;
        event EventHandler<List<TopicPartition>> OnPartitionsRevoked;
        event EventHandler<string> OnStatistics;
    }
}