using System.Collections.Generic;

namespace MySelf.Diab.Model
{
    public class Friend
    {
        public Friend()
        {
            Active = true;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public bool Active { get; set; }

        public List<FriendActivity> FriendActivities { get; set; }
        public List<LogProfile> LogProfiles { get; set; }
    }
}
