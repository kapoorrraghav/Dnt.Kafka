using FizzWare.NBuilder;

namespace Dnt.Kafka.Tests.Core
{
    public static class DataGenerator
    {
        public static TestMessage CreateTestMessage()
        {
            var retVal = Builder<TestMessage>.CreateNew()
                .With(x => x.Address = CreateAddress())
                .Build();

            return retVal;
        }

        public static Address CreateAddress()
        {
            var retVal = Builder<Address>.CreateNew()
                .Build();

            return retVal;
        }
    }
}