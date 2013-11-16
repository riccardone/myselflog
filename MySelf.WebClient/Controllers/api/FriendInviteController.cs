using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MySelf.Diab.Data.Contracts;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.Controllers.api
{
    [DataContract]
    public class SendFriendRequest
    {
        [DataMember(Name = "logprofileid", IsRequired = true)]
        public Guid LogProfileId { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataMember(Name = "message", IsRequired = true)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }

    //public class DateObsessedHandler : DelegatingHandler
    //{
    //    protected async override Task<HttpResponseMessage> SendAsync(
    //        HttpRequestMessage request,
    //        CancellationToken cancellationToken)
    //    {
    //        var requestDate = request.Headers.Date;
    //        // do something with the date ...

    //        var response = await base.SendAsync(request, cancellationToken);

    //        var responseDate = response.Headers.Date;
    //        // do something with the date ...

    //        return response;
    //    }
    //}

    [Authorize]
    public class FriendInviteController : ApiController
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public FriendInviteController(ILogManager logManager, IMapper mapper)
        {
            _logManager = logManager;
            _mapper = mapper;
        }
        
        public HttpResponseMessage Post([FromBody]SendFriendRequest data)
        {
            sendEmail(data);
            return Request.CreateResponse(HttpStatusCode.OK, "message sent");
        }

        private void sendEmail(SendFriendRequest data)
        {
            var message = new MailMessage
            {
                From = new MailAddress("invite@myselflog.com"),
                Subject = "Invite to see some health values",
                Body = string.Format(
                    "Hi, you are invited to see this an health profile. Click on this link to see the profile http://www.myselflog.com/profile/{0}",
                    data.LogProfileId)
            };
            message.To.Add(data.Email);

            var client = new SmtpClient();
            // send credentials if your SMTP server requires them
            //client.Credentials = new NetworkCredential("user", "password");
            client.Send(message);

            //var mail = new MailMessage("EmailFrom", data.Email);

            //const string sendtoBcc = "test@arvixe.com";
            //mail.Bcc.Add(sendtoBcc);

            //mail.Subject = "Subject";
            //mail.IsBodyHtml = true;

            //mail.Body =
            //    string.Format(
            //        "Hi, you are invited to see this an health profile. Click on this link to see the profile http://www.myselflog.com/profile/{0}",
            //        data.LogProfileId);
 
            //// send the message
            //var smtp = new SmtpClient();
            //smtp.Send(mail);
            //// - See more at: http://blog.arvixe.com/send-email-using-jquery-and-web-api-part-2/#sthash.8AVABG12.dpuf
        }
    }
}
