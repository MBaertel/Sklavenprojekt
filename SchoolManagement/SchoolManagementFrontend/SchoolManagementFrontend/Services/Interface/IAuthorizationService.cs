using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface IAuthorizationService
    {
        public IReadOnlyList<string> UserScopes { get; }

        bool HasRole(string role);
        bool HasAllRoles(IEnumerable<string> roles);

        bool IsLoggedIn { get; }
        Guid? CurrentUserId { get; }
        string? CurrentUserName { get; }

        Task LoginAsync(string username, string password);
        Task LogoutAsync();

        event EventHandler<bool> UserLoggedIn;
    }
}
