using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySelf.Diab.Model;

namespace MySelf.WebClient.Models
{
    public interface IMapper
    {
        List<LogDto> ToLogDto(IEnumerable<GlucoseLevel> glucoseLevels);
        List<LogProfileDto> ToLogProfileDto(IEnumerable<LogProfile> logProfiles);
    }

    public class Mapper : IMapper
    {
        public List<LogDto> ToLogDto(IEnumerable<GlucoseLevel> glucoseLevels)
        {
            return glucoseLevels.Select(glucoseLevel => new LogDto
            {
                GlobalId = glucoseLevel.GlobalId,
                LogDate = glucoseLevel.LogDate,
                Message = glucoseLevel.Message,
                Value = glucoseLevel.Value,
                ProfileId = glucoseLevel.LogProfile.GlobalId
            }).ToList();
        }

        public List<LogProfileDto> ToLogProfileDto(IEnumerable<LogProfile> logProfiles)
        {
            return logProfiles.Select(logProfile => new LogProfileDto
            {
                GlobalId = logProfile.GlobalId,
                Name = logProfile.Name,
                Logs = ToLogDto(logProfile.GlucoseLevels)
            }).ToList();
        }
    }
}