﻿using System;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Keycloak.Net.Common.Extensions;
using Keycloak.Net.Services;
using Keycloak.Net.Services.Default;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        private static readonly ISerializer s_serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        });

        static KeycloakClient()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
        }

        private readonly IUrlService _urlService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IForwardedHttpHeadersService _forwardedHttpHeadersService;

        private KeycloakClient(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public KeycloakClient(IUrlService urlService, IUserService userService)
            : this(urlService)
        {
            _userService = userService;
        }

        public KeycloakClient(IUrlService urlService, ITokenService tokenService)
            : this(urlService)
        {
            _tokenService = tokenService;
        }

        public KeycloakClient(IUrlService urlService, IUserService userService, IForwardedHttpHeadersService forwardedHttpHeadersService)
            : this(urlService, userService)
        {
            _forwardedHttpHeadersService = forwardedHttpHeadersService;
        }

        public KeycloakClient(IUrlService urlService, ITokenService tokenService, IForwardedHttpHeadersService forwardedHttpHeadersService)
            : this(urlService, tokenService)
        {
            _forwardedHttpHeadersService = forwardedHttpHeadersService;
        }

        [Obsolete]
        public KeycloakClient(
            string url,
            Func<string> getToken,
            ForwardedHttpHeaders forwardedHttpHeaders)
            : this(new UrlService(url), new TokenService(getToken), new ForwardedHttpHeadersService(forwardedHttpHeaders))
        {
        }


        private IFlurlRequest GetBaseUrl(string authenticationRealm) => new Url(_urlService.Get())
            .AppendPathSegment("/auth")
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithAuthentication(_tokenService.Get, _urlService.Get(),authenticationRealm, _userService.GetUserName(), _userService.GetPassword())
            .WithForwardedHttpHeaders(_forwardedHttpHeadersService.Get());
    }
}
