using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "root")]
    public class RootDto
    {
        [DataMember(Name = "logprofilesasowner")]
        public List<LogProfileDto> LogProfilesAsOwner { get; set; }

        [DataMember(Name = "logprofilesasfriend")]
        public List<LogProfileDto> LogProfilesAsFriend { get; set; }
    }
}