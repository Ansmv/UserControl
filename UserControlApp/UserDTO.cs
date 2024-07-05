using System;
using System.Runtime.Serialization;

namespace UserControlApp
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public uint Id { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string DRFO { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public DateTime LastUpdated { get; set; }
    }

}
