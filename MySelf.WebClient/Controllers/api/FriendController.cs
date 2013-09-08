using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers.api
{
    [DataContract]
    public class AddFriendRequest
    {
        [DataMember(Name = "logprofileid", IsRequired = true)]
        public Guid LogProfileId { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }
    }

    [DataContract]
    public class GetLogAsFriendRequest
    {
        [DataMember(Name = "link", IsRequired = true)]
        public string Link { get; set; }
    }

    [DataContract]
    public class GetLogAsFriendResponse
    {
        [DataMember(Name = "logprofileasfriend", IsRequired = true)]
        public LogProfileDto LogProfileDto { get; set; }
    }
    
    public class FriendController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public FriendController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        public HttpResponseMessage Get([FromUri]GetLogAsFriendRequest data)
        {
            try
            {
                var logProfile = _logManager.ModelReader.GetLogProfile(data.Link);
                if (logProfile == null)
                    throw new ArgumentException("Profile not found");

                if (logProfile.SecurityLink == null)
                    return Request.CreateResponse(HttpStatusCode.Accepted, string.Empty);

                var logProfileDto = _mapper.ToLogProfileDto(new[] {logProfile});

                return Request.CreateResponse(HttpStatusCode.Accepted, new GetLogAsFriendResponse { LogProfileDto = logProfileDto.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody]AddFriendRequest data)
        {
            try
            {
                var logProfile = _logManager.ModelReader.GetLogProfile(data.LogProfileId);
                if (logProfile.Friends.Any(f => f.Email == data.Email))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "the required friendship is already there for this profile");
                }

                var friend = new Friend
                    {
                        Email = data.Email,
                        LogProfiles = new List<LogProfile> {logProfile},
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(Guid globalId)
        {
            try
            {
                _logManager.LogCommands.Delete(globalId);
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
