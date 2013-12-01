using System;

namespace MySelf.Diab.Model
{
    public class FriendActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }

        public Friend Friend { get; set; }
    }
}
