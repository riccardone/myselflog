﻿using System.Collections.Generic;
using System.Linq;
using MySelf.Diab.Model;

namespace MySelf.WebClient.Models
{
    public interface IMapper
    {
        List<LogDto> ToLogsDto(IEnumerable<GlucoseLevel> glucoseLevels);
        List<LogProfileDto> ToLogProfilesDto(IEnumerable<LogProfile> logProfiles);
        List<TerapyDto> ToTerapiesDto(IEnumerable<Terapy> terapies);
        List<FriendDto> ToFriendsDto(IEnumerable<Friend> items);
    }

    public class Mapper : IMapper
    {
        public List<LogDto> ToLogsDto(IEnumerable<GlucoseLevel> glucoseLevels)
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

        public List<FriendDto> ToFriendsDto(IEnumerable<Friend> items)
        {
            return items.Select(f => new FriendDto
            {
                Active = f.Active,
                Email = f.Email
            }).ToList();
        }

        public List<LogProfileDto> ToLogProfilesDto(IEnumerable<LogProfile> logProfiles)
        {
            return logProfiles.Select(logProfile => new LogProfileDto
            {
                GlobalId = logProfile.GlobalId,
                Name = logProfile.Name,
                Logs = ToLogsDto(logProfile.GlucoseLevels ?? new List<GlucoseLevel>()),
                Friends = ToFriendsDto(logProfile.Friends),
                Terapies = ToTerapiesDto(logProfile.Terapies ?? new List<Terapy>()),
                Foods = ToFoodsDto(logProfile.Foods ?? new List<Food>()),
                SecurityLink = logProfile.SecurityLink != null ? BuildSecurityLink(logProfile.SecurityLink.Link) : string.Empty
            }).ToList();
        }

        private static string BuildSecurityLink(string value)
        {
            return $"diary/{value}";
        }

        public List<FoodDto> ToFoodsDto(IEnumerable<Food> items)
        {
            return items.Select(i => new FoodDto
            {
                GlobalId = i.GlobalId,
                LogDate = i.LogDate,
                Message = i.Message,
                Calories = i.Calories,
                ProfileId = i.LogProfile.GlobalId,
                FoodTypes = i.FoodTypes
            }).ToList();
        }

        public List<TerapyDto> ToTerapiesDto(IEnumerable<Terapy> terapies)
        {
            return terapies.Select(i => new TerapyDto
            {
                TerapyGlobalId = i.GlobalId,
                LogDate = i.LogDate,
                Message = i.Message,
                TerapyValue = i.Value,
                ProfileId = i.LogProfile.GlobalId,
                IsSlow = i.IsSlow
            }).ToList();
        }
    }
}