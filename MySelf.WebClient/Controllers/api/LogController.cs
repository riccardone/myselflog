using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Web.WebPages.OAuth;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Filters;
using MySelf.WebClient.Models;
using WebMatrix.WebData;

namespace MySelf.WebClient.Controllers.api
{
    [Authorize]
    public class LogController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public LogController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var results = new RootDto
                {
                    LogProfilesAsOwner = _mapper.ToLogProfilesDto(_logManager.ModelReader.GetLogProfilesAsOwner(User.Identity.Name)),
                    LogProfilesAsFriend = _mapper.ToLogProfilesDto(_logManager.ModelReader.GetLogProfilesAsFriend(User.Identity.Name))
                };
                if (results.LogProfilesAsFriend.Count == 0 && results.LogProfilesAsOwner.Count == 0)
                {
                    WebSecurity.Logout();
                    throw new Exception("for some reasons you don't have any profiles");
                }
                    
                return Request.CreateResponse(HttpStatusCode.Accepted, results, "application/json");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var logProfiles =
                    _mapper.ToLogProfilesDto(_logManager.ModelReader.GetLogProfilesAsOwner(User.Identity.Name), 3);
                if (logProfiles == null || logProfiles.Count == 0)
                    throw new ArgumentException("Profile not found");

                return Request.CreateResponse(HttpStatusCode.Accepted, logProfiles.First(), "application/json");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        // POST api/log
        public HttpResponseMessage Post([FromBody]LogDto data)
        {
            try
            {
                var logMessage = new LogMessage
                {
                    Email = User.Identity.Name,
                    Value = string.IsNullOrWhiteSpace(data.Message) == false ? int.Parse(data.Message) : 0,
                    Message = string.Empty,
                    LogDate = data.LogDate,
                    ProfileId = data.ProfileId,
                    IsSlow = data.IsSlow,
                    TerapyValue = data.TerapyValue
                };

                _logManager.LogCommands.AddLogMessage(logMessage);
                _logManager.Save();
                data.GlobalId = logMessage.GlobalId;
                data.TerapyGlobalId = logMessage.TerapyGlobalId;
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }

        // DELETE api/log/5
        [HttpDelete]
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
