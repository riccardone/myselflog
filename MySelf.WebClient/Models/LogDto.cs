using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "log")]
    public class LogDto
    {
        [Required]
        [DataMember(Name = "value", IsRequired = true)]
        public int Value { get; set; }

        
        [DataMember(IsRequired = false, Name = "message")]
        public string Message { get; set; }

        [DataMember(IsRequired = true, Name = "logdate")]
        [DataType(DataType.DateTime)]
        public DateTime LogDate { get; set; }

        [DataMember(Name = "globalid")]
        public Guid GlobalId { get; set; }

        [DataMember(Name = "profileid")]
        public Guid ProfileId { get; set; }
    }
}