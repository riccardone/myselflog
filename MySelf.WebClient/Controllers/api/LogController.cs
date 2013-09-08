using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Models;

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
                        LogProfilesAsOwner = _mapper.ToLogProfileDto(_logManager.ModelReader.GetLogProfilesAsOwner(User.Identity.Name)),
                        LogProfilesAsFriend = _mapper.ToLogProfileDto(_logManager.ModelReader.GetLogProfilesAsFriend(User.Identity.Name))
                    };

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
                var logProfile =
                    _mapper.ToLogProfileDto(_logManager.ModelReader.GetLogProfilesAsOwner(User.Identity.Name));
                if (logProfile == null)
                    throw new ArgumentException("Profile not found");
                return Request.CreateResponse(HttpStatusCode.Accepted, logProfile.First(), "application/json");
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
                var newId = Guid.NewGuid();
                var logMessage = new LogMessage
                    {
                        Email = User.Identity.Name,
                        Value = data.Value,
                        Message = data.Message,
                        LogDate = data.LogDate,
                        GlobalId = newId,
                        ProfileId = data.ProfileId 
                    };
                _logManager.LogCommands.AddLogMessage(logMessage);
                _logManager.Save();
                data.GlobalId = newId;
                return Request.CreateResponse(HttpStatusCode.Created, data);
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
