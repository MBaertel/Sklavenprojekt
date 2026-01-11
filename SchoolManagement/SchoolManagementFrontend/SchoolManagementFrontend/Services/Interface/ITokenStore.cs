using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface ITokenStore
    {
        public Task SaveTokenAsync(string key,string token);
        public Task<string> GetTokenAsync(string key);
        
        public Task<bool> HasToken(string key);
        public Task ClearTokens();
    }
}
