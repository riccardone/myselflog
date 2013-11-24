using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using MySelf.WebClient.Filters;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers.api
{
    [Authorize]
    public class TerapyController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public TerapyController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }

        // DELETE api/terapy/5
        [HttpDelete]
        public HttpResponseMessage Delete(Guid globalId)
        {
            try
            {
                _logManager.LogCommands.DeleteTerapy(globalId);
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
