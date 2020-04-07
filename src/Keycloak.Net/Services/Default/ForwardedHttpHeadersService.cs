namespace Keycloak.Net.Services.Default
{
    internal class ForwardedHttpHeadersService : IForwardedHttpHeadersService
    {
        private readonly ForwardedHttpHeaders _forwardedHttpHeaders;

        public ForwardedHttpHeadersService(ForwardedHttpHeaders forwardedHttpHeaders)
        {
            _forwardedHttpHeaders = forwardedHttpHeaders;
        }

        public ForwardedHttpHeaders Get()
        {
            return _forwardedHttpHeaders;
        }
    }
}
