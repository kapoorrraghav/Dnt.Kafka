using System.Runtime.Serialization;

namespace Dnt.Kafka.Tests.Core
{
    [DataContract]
    public class TestMessage
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
        [DataMember]
        public decimal AccountBalance { get; set; }
        [DataMember]
        public Address Address { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string StreetName { get; set; }
        [DataMember]
        public string PinCode { get; set; }
    }
}
