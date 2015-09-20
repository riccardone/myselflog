﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "log")]
    public class LogDto
    {
        [DataMember(Name = "medicalvalue")]
        public string MedicalValue { get; set; }
        
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

        [DataMember(Name = "globalid")]
        public Guid? GlobalId { get; set; }

        [DataMember(Name = "terapyglobalid")]
        public Guid? TerapyGlobalId { get; set; }
    }
}