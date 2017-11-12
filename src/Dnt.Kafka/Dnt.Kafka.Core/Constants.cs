namespace Dnt.Kafka.Core
{
    public static class Constants
    {
        public static class KafkaConfiguration
        {
            public const string GroupId = "group.id";
            public const string BootstrapServers = "bootstrap.servers";
            public const string EnableAutoCommit = "enable.auto.commit";
            public const string AutoCommitInterval = "auto.commit.interval.ms";
            public const string StatisticsInterval = "statistics.interval.ms";

            public static class Security
            {
                public const string Protocol = "security.protocol";
                public const string SslCaLocation = "ssl.ca.location";
                public const string SslCaPassword = "ssl.ca.password";
                public const string SslCertificateLocation = "ssl.certificate.location";
                public const string SslKeyLocation = "ssl.key.location";
                public const string SslKeyPassword = "ssl.key.password";
            }
        }
    }
}
