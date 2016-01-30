using System;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using System.Linq;
using System.Data.Entity;

namespace MySelf.Diab.Data
{
    public class LogCommands : ILogCommands
    {
        private readonly DiabDbContext _dbContext;

        public LogCommands(IDatabaseFactory databaseFactory)
        {
            _dbContext = databaseFactory.Get() as DiabDbContext;
        }

        public void AddLogMessage(LogMessage logMessage)
        {
            var person = _dbContext.People.Include(l => l.LogProfiles).FirstOrDefault(p => p.Email == logMessage.Email);
            if (person == null)
                throw new ArgumentException("person not found");
            
            var logProfile = person.LogProfiles.FirstOrDefault(p => p.GlobalId == logMessage.ProfileId); 
            if (logProfile == null)
                throw new ArgumentException("logProfile not found");

            AddGlocoseLevel(logMessage, logProfile);
            AddTerapy(logMessage, logProfile);
            AddFood(logMessage, logProfile);
        }

        private void AddFood(LogMessage logMessage, LogProfile logProfile)
        {
            if (logMessage.Calories <= 0) return;
            var newFoodId = Guid.NewGuid();
            var item = new Food
            {
                GlobalId = newFoodId,
                LogDate = logMessage.LogDate,
                LogProfile = logProfile,
                Message = logMessage.Message,
                Calories = logMessage.Calories,
                FoodTypes = logMessage.FoodTypes
            };
            _dbContext.Foods.Add(item);
        }

        private void AddTerapy(LogMessage logMessage, LogProfile logProfile)
        {
            if (logMessage.TerapyValue <= 0) return;
            var newTerapyId = Guid.NewGuid();
            var terapy = new Terapy
                {
                    GlobalId = newTerapyId,
                    IsSlow = logMessage.IsSlow,
                    LogDate = logMessage.LogDate,
                    LogProfile = logProfile,
                    Message = logMessage.Message,
                    Value = logMessage.TerapyValue
                };
            _dbContext.Terapies.Add(terapy);
            logMessage.TerapyGlobalId = newTerapyId;
        }

        private void AddGlocoseLevel(LogMessage logMessage, LogProfile logProfile)
        {
            if (logMessage.Value <= 0) return;
            var newLogId = Guid.NewGuid();
            var glucoseLevel = new GlucoseLevel
                {
                    LogDate = logMessage.LogDate,
                    Message = logMessage.Message,
                    Value = logMessage.Value,
                    GlobalId = newLogId,
                    LogProfile = logProfile
                };
            _dbContext.GlucoseLevels.Add(glucoseLevel);
            logMessage.GlobalId = newLogId;
        }

        public void Delete(Guid globalId)
        {
            var itemToDelete = _dbContext.GlucoseLevels.FirstOrDefault(g => g.GlobalId == globalId);
            if (itemToDelete != null)
            {
                _dbContext.GlucoseLevels.Remove(itemToDelete);
            }
        }

        public void AddLogProfile(string name, Guid globalId, string email)
        {
            var person = _dbContext.People.Include(l => l.LogProfiles).FirstOrDefault(p => p.Email == email);
            if (person == null || person.LogProfiles.Any(lp => lp.Name == name))
                throw new ArgumentException("error creating logprofile");
            var logProfile = new LogProfile
                {
                    Name = name,
                    GlobalId = globalId,
                    Person = person
                };
            _dbContext.LogProfiles.Add(logProfile);
        }


        public void AddLogProfile(string name, Guid globalId, Person owner)
        {
            var logProfile = new LogProfile
            {
                Name = name,
                GlobalId = globalId,
                Person = owner
            };
            _dbContext.LogProfiles.Add(logProfile);
        }

        public void AddLogProfile(LogProfile logProfile)
        {
            _dbContext.LogProfiles.Add(logProfile);
        }

        public void DeleteTerapy(Guid globalid)
        {
            var itemToDelete = _dbContext.Terapies.FirstOrDefault(a => a.GlobalId == globalid);
            if (itemToDelete != null)
                _dbContext.Terapies.Remove(itemToDelete);
        }
    }
}
