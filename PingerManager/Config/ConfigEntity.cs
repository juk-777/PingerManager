using System.Runtime.Serialization;

namespace PingerManager.Config
{
    [DataContract]
    public class ConfigEntity
    {
        [DataMember]
        public string Host { get; set; }
        [DataMember]
        public int Period { get; set; }
        [DataMember]
        public string Protocol { get; set; }
        [DataMember]
        public int Port { get; set; }
        [DataMember]
        public int ValidStatusCode { get; set; }
    }
}
