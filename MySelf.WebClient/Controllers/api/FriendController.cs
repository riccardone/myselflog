using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Filters;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers.api
{
    [DataContract]
    public class AddFriendRequest
    {
        [DataMember(Name = "logprofileid", IsRequired = true)]
        public Guid LogProfileId { get; set; }
        
        [DataMember(Name = "email", IsRequired = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    //[DataContract]
    //public class RemoveFriendRequest
    //{
    //    [DataMember(Name = "logprofileid", IsRequired = true)]
    //    public Guid LogProfileId { get; set; }

    //    //[DataType(DataType.EmailAddress)]
    //    [DataMember(Name = "email", IsRequired = true)]
    //    public string Email { get; set; }
    //}

    [DataContract]
    public class GetLogsAsFriendRequest
    {
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }
    }

    [DataContract]
    public class GetLogsAsFriendResponse 
    {
        [DataMember(Name = "logsprofileasfriend", IsRequired = true)]
        public List<LogProfileDto> LogsProfileDto { get; set; }
    }

    [Authorize]
    public class FriendController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public FriendController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        public HttpResponseMessage Get([FromUri]GetLogsAsFriendRequest data)
        {
            try
            {
                var logProfiles = _logManager.ModelReader.GetLogProfilesAsFriend(data.Email);
                var results = _mapper.ToLogProfileDto(logProfiles);

                return Request.CreateResponse(HttpStatusCode.Accepted, new GetLogsAsFriendResponse { LogsProfileDto = results });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }
        
        public HttpResponseMessage Post([FromBody]AddFriendRequest data)
        {
            try
            { 
                var logProfile = _logManager.ModelReader.GetLogProfile(data.LogProfileId);

                if (logProfile == null)
                    return Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "profile not found");
                if (logProfile.Friends.Any(f => f.Email == data.Email))
                    return Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "it's already a friend for this profile");

                var friend = new Friend
                    {
                        Email = data.Email,
                        LogProfiles = new List<LogProfile> {logProfile},
                        Active = true,
                        FriendActivities = new List<FriendActivity>
                            {
                                new FriendActivity
                                    {
                                        ActivityDate = DateTime.Now,
                                        Description = "Friend created",
                                    }
                            }
                    };

                _logManager.FriendCommands.Create(friend);
                _logManager.Save();
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string email, Guid logprofileid)
        {
            try
            {
                _logManager.FriendCommands.Delete(email, logprofileid);
                _logManager.Save();

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
            }
        }
    }
}
