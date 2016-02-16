using System;
using System.Linq;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using System.Data.Entity;

namespace MySelf.Diab.Data
{
    public class PersonCommands : IPersonCommands 
    {
        private readonly DiabDbContext _db;

        public PersonCommands(IDatabaseFactory databaseFactory)
        {
            _db = databaseFactory.Get();
        }

        //public void CreateOrUpdate(Person item)
        //{
        //    var currentUser = _db.People.Include(e => e.LogProfiles).FirstOrDefault(p => p.Email == item.Email);
            
        //    if (currentUser != null)
        //    {
        //        currentUser.Country = item.Country;
        //        currentUser.DateOfBirth = item.DateOfBirth;
        //        currentUser.FirstName = item.FirstName;
        //        currentUser.LastName = item.LastName;
        //        currentUser.PostalCode = item.PostalCode;
        //    }
        //    else
        //    {
        //        currentUser = new Person
        //            {
        //                Email = item.Email,
        //                Country = item.Country,
        //                DateOfBirth = item.DateOfBirth,
        //                FirstName = item.FirstName,
        //                LastName = item.LastName,
        //                PostalCode = item.PostalCode
        //            };
        //        _db.People.Add(currentUser);
        //    }

        //    if (currentUser.LogProfiles.Count != 0) return;
        //    var logProfile = new LogProfile
        //        {
        //            Name = LogProfile.DefaultName,
        //            GlobalId = Guid.NewGuid(),
        //            Person = currentUser
        //        };
        //    _db.LogProfiles.Add(logProfile);
        //}

        public void Create(Person item)
        {
            var p = new Person
            {
                Email = item.Email,
                Country = item.Country,
                DateOfBirth = item.DateOfBirth,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PostalCode = item.PostalCode
            };
            _db.People.Add(p);
        }

        public void Update(Person item)
        {
            var currentUser = _db.People.Include(e => e.LogProfiles).FirstOrDefault(p => p.Email == item.Email);

            if (currentUser != null)
            {
                currentUser.Country = item.Country;
                currentUser.DateOfBirth = item.DateOfBirth;
                currentUser.FirstName = item.FirstName;
                currentUser.LastName = item.LastName;
                currentUser.PostalCode = item.PostalCode;
            }
        }
    }
}
