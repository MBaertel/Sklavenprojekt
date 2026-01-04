using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Mock
{
    public class MockTokenStore : ITokenStore
    {
        private Dictionary<string, string> tokens = new Dictionary<string, string>();
        
        public Task ClearTokens()
        {
            tokens.Clear();
            return Task.CompletedTask;
        }

        public Task<string> GetTokenAsync(string key)
        {
            if(tokens.TryGetValue(key,out string value))
            {
                return Task.FromResult(tokens[key]);
            }
            return Task.FromResult(string.Empty);
        }

        public Task<bool> HasToken(string key)
        {
            return Task.FromResult(tokens.ContainsKey(key));
        }

        public Task SaveTokenAsync(string key, string token)
        {
            tokens[key] = token;
            return Task.CompletedTask;
        }
    }
}
