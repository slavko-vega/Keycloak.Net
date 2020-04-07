using System.IO;
using Keycloak.Net.Services;
using Microsoft.Extensions.Configuration;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        private readonly KeycloakClient _client;
        
        public KeycloakClientShould()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string url = configuration["url"];
            string userName = configuration["userName"];
            string password = configuration["password"];

            _client = new KeycloakClient(new UrlService(url), new UserService(userName, password));
        }

        private class UrlService :IUrlService
        {
            private readonly string _url;

            public UrlService(string url)
            {
                _url = url;
            }
            public string Get()
            {
                return _url;
            }
        }

        private class UserService: IUserService
        {
            private readonly string _password;
            private readonly string _userName;

            public UserService(string userName, string password)
            {
                _userName = userName;
                _password = password;
            }

            public string GetUserName()
            {
                return _userName;
            }

            public string GetPassword()
            {
                return _password;
            }
        }
    }
}
