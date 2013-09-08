using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "logprofile")]
    public class LogProfileDto
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "globalid")]
        public Guid GlobalId { get; set; }

        [DataMember(Name = "logs")]
        public List<LogDto> Logs { get; set; }
    }
}