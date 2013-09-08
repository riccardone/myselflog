using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;

namespace MySelf.WebClient.Controllers.api
{
    [DataContract]
    public class SecurityLinkRequest
    {
        [DataMember(Name = "logprofileid", IsRequired = true)]
        public Guid LogProfileId { get; set; }
    }

    [DataContract]
    public class SecurityLinkResponse
    {
        [DataMember(Name = "logprofileid", IsRequired = true)]
        public Guid LogProfileId { get; set; }

        [DataMember(Name = "link", IsRequired = true)]
        public string Link { get; set; }
    }

    [Authorize]
    public class SecurityLinkController : ApiController
    {
        private readonly ILogManager _logManager;
        

        public SecurityLinkController(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public HttpResponseMessage Get([FromUri]SecurityLinkRequest data)
        {
            try
            {
                var logProfile = _logManager.ModelReader.GetLogProfile(data.LogProfileId);
                if (logProfile == null)
                    throw new ArgumentException("Profile not found");

                if (logProfile.SecurityLink == null)
                    return Request.CreateResponse(HttpStatusCode.Accepted, string.Empty);

                return Request.CreateResponse(HttpStatusCode.Accepted,
                                              new SecurityLinkResponse
                                                  {
                                                      LogProfileId = data.LogProfileId,
                                                      Link = BuildSecurityLink(logProfile.SecurityLink.Link)
                                                  });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        private string BuildSecurityLink(string value)
        {
            return string.Format("diary/{0}", value);
        }

        // POST api/log
        public HttpResponseMessage Post([FromBody]SecurityLinkRequest data)
        {
            try
            {
                var logProfile = _logManager.ModelReader.GetLogProfile(data.LogProfileId);
                if (logProfile == null)
                    throw new ArgumentException("Profile not found");

                _logManager.SecurityLinkCommands.SetNewShareLink(data.LogProfileId);
                _logManager.Save();

                logProfile = _logManager.ModelReader.GetLogProfile(data.LogProfileId);

                return Request.CreateResponse(HttpStatusCode.Created, new SecurityLinkResponse
                {
                    LogProfileId = data.LogProfileId,
                    Link = BuildSecurityLink(logProfile.SecurityLink.Link)
                });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        // DELETE api/log/5
        public HttpResponseMessage Delete(Guid globalId)
        {
            try
            {
                _logManager.SecurityLinkCommands.RemoveShareLink(globalId);
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
