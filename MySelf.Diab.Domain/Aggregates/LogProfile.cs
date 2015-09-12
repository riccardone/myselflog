using CrossCutting.DomainBase;
using MySelf.Diab.Domain.Aggregates.ValueObjects;
using MySelf.Diab.Domain.Events;
using System.Collections.Generic;

namespace MySelf.Diab.Domain.Aggregates
{
    public class LogProfile : AggregateBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PersonId { get; set; }
        public SecurityLink SecurityLink { get; set; }
        public List<string> FriendsIds { get; set; }
        public List<GlucoseLevel> GlucoseLevels { get; set; }
        public List<Terapy> Terapies { get; set; }

        public override string AggregateId
        {
            get
            {
                return Id;
            }
        }

        public LogProfile(string id, string name, string personId) : this()
        {
            RaiseEvent(new LogProfileCreated(id, name, personId));
        }
        public LogProfile()
        {
            RegisterTransition<LogProfileCreated>(Apply);
            RegisterTransition<GlucoseLevelAdded>(Apply);
            RegisterTransition<TerapyAdded>(Apply);
        }

        private void Apply(TerapyAdded obj)
        {
            Terapies.Add(new Terapy { IsSlow = obj.IsSlow, LogDate = obj.LogDate, Message = obj.Message, Value = obj.Value });
        }

        private void Apply(GlucoseLevelAdded obj)
        {
            GlucoseLevels.Add(new GlucoseLevel { LogDate = obj.LogDate, Message = obj.Message, Value = obj.Value });
        }

        private void Apply(LogProfileCreated obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            PersonId = obj.PersonId;
            Terapies = new List<Terapy>();
            GlucoseLevels = new List<GlucoseLevel>();
        }

        public static LogProfile CreateLogProfile(string id, string name, string personId)
        {
            return new LogProfile(id, name, personId);
        }
    }
}
