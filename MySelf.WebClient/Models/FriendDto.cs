using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySelf.WebClient.Models
{
    [DataContract(Name = "friend")]
    public class FriendDto
    {
        //[DataMember(Name = "firstname")]
        //public string FirstName { get; set; }

        //[DataMember(Name = "lastname")]
        //public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        //[DataMember(Name = "country")]
        //public string Country { get; set; }

        //[DataMember(Name = "postalcode")]
        //public string PostalCode { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }
    }
}