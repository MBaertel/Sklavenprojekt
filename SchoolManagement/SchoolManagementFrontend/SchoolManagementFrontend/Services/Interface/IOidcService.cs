using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface IOidcService
    {
        public Task<bool> HasStoredCredentials();

        public Task<bool> TryRefresh();

        public Task<bool> Login();
    }
}
