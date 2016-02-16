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
    
    public class FriendWithLinkController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public FriendWithLinkController(ILogManager logManager, IMapper mapper)
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

                var logProfileDto = _mapper.ToLogProfilesDto(new[] {logProfile});

                return Request.CreateResponse(HttpStatusCode.Accepted, new GetLogAsFriendResponse { LogProfileDto = logProfileDto.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }
    }
}
