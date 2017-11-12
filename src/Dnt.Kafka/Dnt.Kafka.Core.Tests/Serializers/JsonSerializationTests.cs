using Dnt.Kafka.Core.Serializers;
using Dnt.Kafka.Tests.Core;
using System;
using Xunit;

namespace Dnt.Kafka.Core.Tests.Serializers
{
    public class JsonSerializationTests
    {
        [Theory]
        [InlineData(new byte[0])]
        [InlineData(new byte[] { 1, 2 })]
        [InlineData(new byte[] { 1, 2, 3, 4, 5 })]
        public void ShouldReconstructByteArray(byte[] data)
        {
            var sut1 = new JsonSerializer<byte[]>();
            var sut2 = new JsonDeserializer<byte[]>();

            Assert.Equal(data, sut2.Deserialize(sut1.Serialize(data)));
        }

        [Fact]
        public void ShouldReconstructComplexType()
        {
            var data = DataGenerator.CreateTestMessage();

            var sut1 = new JsonSerializer<TestMessage>();
            var sut2 = new JsonDeserializer<TestMessage>();

            Assert.True(data.JsonComapre(sut2.Deserialize(sut1.Serialize(data))));
        }

        [Fact]
        public void ShouldThrowExceptionOnSerializeNullData()
        {
            var sut = new JsonSerializer<TestMessage>();

            Assert.Throws<ArgumentNullException>(() => sut.Serialize(null));
        }

        [Fact]
        public void ShouldThrowExceptionOnDeserializeNullData()
        {
            var sut = new JsonDeserializer<TestMessage>();

            Assert.Throws<ArgumentNullException>(() => sut.Deserialize(null));
        }
    }
}