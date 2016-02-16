using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "log")]
    public class TerapyDto
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(IsRequired = true, Name = "logdate")]
        [DataType(DataType.DateTime)]
        public DateTime LogDate { get; set; }

        [DataMember(Name = "profileid")]
        public Guid ProfileId { get; set; }

        [DataMember(Name = "isslow")]
        public bool IsSlow { get; set; }

        [DataMember(Name = "terapyvalue")]
        public int TerapyValue { get; set; }

        [DataMember(Name = "terapyglobalid")]
        public Guid? TerapyGlobalId { get; set; }
    }
}