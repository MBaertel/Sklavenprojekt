using Microsoft.JSInterop;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services
{
    public class BrowserAuthService : IOidcService
    {
        private readonly ITokenStore _tokenStore;
        private readonly IJSRuntime _jsRuntime;

        public BrowserAuthService(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public async Task<bool> HasStoredCredentials()
        {
            return await _tokenStore.HasToken("access");
        }

        public async Task<bool> Login()
        {
            var accessToken = await _jsRuntime.InvokeAsync<string>("loginWithOidc");

            if(!string.IsNullOrEmpty(accessToken))
            {
                await _tokenStore.SaveTokenAsync("access",accessToken);
                return true;
            }
            return false;
        }

        public async Task<bool> TryRefresh()
        {
            try
            {
                // oidc-client-ts automatically uses silent renew if configured
                string accessToken = await _jsRuntime.InvokeAsync<string>("silentRenew");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    await _tokenStore.SaveTokenAsync("access", accessToken);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
