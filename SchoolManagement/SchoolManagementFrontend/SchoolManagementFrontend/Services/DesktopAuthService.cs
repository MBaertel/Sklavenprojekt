using Duende.IdentityModel.OidcClient;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services
{
    public class DesktopAuthService : IAuthenticator
    {
        private readonly ITokenStore _tokenStore;
        private readonly OidcClient _oidcClient;

        public DesktopAuthService(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;

            var options = new OidcClientOptions
            {
                Authority = "http://127.0.0.1:8990/",
                ClientId = "schoolAppDesktop",
                RedirectUri = "http://127.0.0.1:7890/",
                Scope = "openid profile offline_access api",
                Browser = new DesktopBrowser(7890)
            };

            _oidcClient = new OidcClient(options);
        }

        public async Task<bool> HasStoredCredentials()
        {
            return await _tokenStore.HasToken("access");
        }

        public async Task<bool> TryRefresh()
        {
            var refreshToken = await _tokenStore.GetTokenAsync("refresh");
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            try
            {
                var result = await _oidcClient.RefreshTokenAsync(refreshToken);

                if (result.IsError)
                    return false;

                await _tokenStore.SaveTokenAsync("access", result.AccessToken);

                if (!string.IsNullOrEmpty(result.RefreshToken))
                {
                    await _tokenStore.SaveTokenAsync("refresh", result.RefreshToken);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Login()
        {
            var request = new LoginRequest();
            var result = await _oidcClient.LoginAsync(request);

            if (result.IsError)
                return false;

            await _tokenStore.SaveTokenAsync("access", result.AccessToken);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                await _tokenStore.SaveTokenAsync("refresh", result.RefreshToken);
            }
            return true;
        }
    }
}
