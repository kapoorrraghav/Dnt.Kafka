﻿using Dnt.Kafka.Core.Serializers;
using Dnt.Kafka.Tests.Core;
using System;
using Xunit;

namespace Dnt.Kafka.Core.Tests.Serializers
{
    public class AvroSerializationTests
    {
        [Theory]
        [InlineData(new byte[0])]
        [InlineData(new byte[] { 1, 2 })]
        [InlineData(new byte[] { 1, 2, 3, 4, 5 })]
        public void ShouldReconstructByteArray(byte[] data)
        {
            var sut1 = new AvroSerializer<byte[]>();
            var sut2 = new AvroDeserializer<byte[]>();

            Assert.Equal(data, sut2.Deserialize(sut1.Serialize(data)));
        }

        [Fact]
        public void ShouldReconstructComplexType()
        {
            var data = DataGenerator.CreateTestMessage();

            var sut1 = new AvroSerializer<TestMessage>();
            var sut2 = new AvroDeserializer<TestMessage>();

            Assert.True(data.JsonComapre(sut2.Deserialize(sut1.Serialize(data))));
        }

        [Fact]
        public void ShouldThrowExceptionOnSerializeNullData()
        {
            var sut = new AvroSerializer<TestMessage>();

            Assert.Throws<ArgumentNullException>(() => sut.Serialize(null));
        }

        [Fact]
        public void ShouldThrowExceptionOnDeserializeNullData()
        {
            var sut = new AvroDeserializer<TestMessage>();

            Assert.Throws<ArgumentNullException>(() => sut.Deserialize(null));
        }
    }
}