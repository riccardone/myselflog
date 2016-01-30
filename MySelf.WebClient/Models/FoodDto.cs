using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "food")]
    public class FoodDto
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(IsRequired = true, Name = "logdate")]
        [DataType(DataType.DateTime)]
        public DateTime LogDate { get; set; }
        [DataMember(Name = "profileid")]
        public Guid ProfileId { get; set; }
        [DataMember(Name = "calories")]
        public int Calories { get; set; }
        [DataMember(Name = "foodtypes")]
        public string FoodTypes { get; set; }
        [DataMember(Name = "foodglobalid")]
        public Guid GlobalId { get; set; }
    }
}