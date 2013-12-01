using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySelf.WebClient.Controllers.api
{
    public class UserController : ApiController
    {
        [Authorize(Roles = "User, Supervisor, Admin")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, User.Identity.Name, "application/json");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.GetBaseException());
            }
        }
    }
}
