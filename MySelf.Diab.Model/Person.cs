using System;
using System.Collections.Generic;

namespace MySelf.Diab.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<LogProfile> LogProfiles { get; set; }
        public Person()
        {
            LogProfiles = new List<LogProfile>();
        }
    }
}
