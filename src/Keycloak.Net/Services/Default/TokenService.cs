using System;

namespace Keycloak.Net.Services.Default
{
    internal class TokenService : ITokenService
    {
        private readonly Func<string> _getToken;

        public TokenService(Func<string> getToken)
        {
            _getToken = getToken;
        }
        public string Get()
        {
            return _getToken();
        }
    }
}
