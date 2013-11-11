﻿using System;
using System.Linq;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data
{
    public class FriendCommands : IFriendCommands 
    {
        private readonly DiabDbContext _db;

        public FriendCommands(IDatabaseFactory databaseFactory)
        {
            _db = databaseFactory.Get();
        }

        public void Create(Friend item)
        {
            _db.Friends.Add(item);
        }

        public void Delete(string email, Guid globalId)
        {
            var profile = _db.LogProfiles.Include("Friends").FirstOrDefault(p => p.GlobalId == globalId);
            if (profile == null)
            {
                return;
            }
            var friend = profile.Friends.FirstOrDefault(f => f.Email == email);
            if (friend == null)
            {
                return;
            }
            _db.Friends.Remove(friend);
        }
    }
}
