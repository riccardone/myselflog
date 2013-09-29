using System.Configuration;
using Microsoft.Web.WebPages.OAuth;

namespace MySelf.WebClient
{
    public static class AuthConfig
    {
        class FbClient
        {
            public FbClient()
            {
                var values = ConfigurationManager.AppSettings["FacebookClient"].Split(';');
                AppId = values[0];
                AppSecret = values[1];
            }

            public string AppId { get; set; }
            public string AppSecret { get; set; }
        }

        public static void RegisterAuth()
        {
            var fbClient = new FbClient();

            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            // prod
            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "262309163906980",
            //    appSecret: "3439950f4bf262d8041e28cdb1b7416c");

            // dev
            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "170633953128198",
            //    appSecret: "2a94e68adc55ff563a8c4ff178a85662");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: fbClient.AppId,
                appSecret: fbClient.AppSecret);

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
