using System.Configuration;

namespace MySelf.WebClient.Code
{
    public class ConfigSettings : IConfigSettings
    {
        public string BaseAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BaseAddress"];
            }
        }
    }
}