using Newtonsoft.Json;

namespace Dnt.Kafka.Tests.Core
{
    public static class Extensions
    {
        public static bool JsonComapre(this object to, object with)
        {
            if (ReferenceEquals(to, with))
                return true;

            if (to == null || with == null)
                return false;

            if (to.GetType() != with.GetType())
                return false;

            var toJSon = JsonConvert.SerializeObject(to);
            var withJson = JsonConvert.SerializeObject(with);

            return toJSon == withJson;
        }
    }
}