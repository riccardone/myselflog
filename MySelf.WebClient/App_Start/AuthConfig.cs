using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MySelf.WebClient.Models;

namespace MySelf.WebClient
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            // prod
            OAuthWebSecurity.RegisterFacebookClient(
                appId: "262309163906980",
                appSecret: "3439950f4bf262d8041e28cdb1b7416c");

            // dev
            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "170633953128198",
            //    appSecret: "2a94e68adc55ff563a8c4ff178a85662");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
