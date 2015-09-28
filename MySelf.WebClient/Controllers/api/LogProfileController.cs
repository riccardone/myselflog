using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers.api
{
    [Authorize]
    public class LogProfileController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public LogProfileController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        // POST api/log
        public HttpResponseMessage Post([FromBody]string name)
        {
            try
            {
                var profile = new LogProfile() { Name = name };

                _logManager.LogCommands.AddLogProfile(profile);
                _logManager.Save();
                
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }
    }
}
