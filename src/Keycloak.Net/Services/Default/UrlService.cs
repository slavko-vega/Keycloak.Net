using System;
using System.Collections.Generic;
using System.Text;

namespace Keycloak.Net.Services.Default
{
    internal class UrlService : IUrlService
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
}
