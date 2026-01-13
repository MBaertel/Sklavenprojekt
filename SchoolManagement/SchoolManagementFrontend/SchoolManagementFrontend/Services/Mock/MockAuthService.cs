using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Mock
{
    internal class MockAuthService : IAuthenticator
    {
        private readonly HashSet<string> _userScopes = new();
        private bool _userLoggedIn = false;

        public IReadOnlyList<string> UserScopes => _userScopes.ToList().AsReadOnly();
        public Guid? CurrentUserId { get; private set; }
        public string? CurrentUserName { get; private set; }

        public event EventHandler<bool> UserLoggedIn;

        public bool IsLoggedIn
        {
            get => _userLoggedIn;
            set
            {
                if (_userLoggedIn != value)
                {
                    _userLoggedIn = value;
                    UserLoggedIn?.Invoke(this, value);
                }
            }
        }

        public bool HasRole(string scope)
        {
            return _userScopes.Any(g => scope == g || scope.StartsWith(g + "."));
        }

        public bool HasAllRoles(IEnumerable<string> scopes)
        {
            return _userScopes.All(g => HasRole(g));
        }

        public async Task<bool> Login()
        {
            CurrentUserId = Guid.NewGuid();

            _userScopes.Clear();
            _userScopes.Add("user.read");
            _userScopes.Add("exams.read");

            await Task.Delay(500);
            IsLoggedIn = true;
            return true;
        }

        public async Task LogoutAsync()
        {
            CurrentUserId = null;
            CurrentUserName = null;
            _userScopes.Clear();

            await Task.Delay(50);
            IsLoggedIn = false;
        }

        public async Task<bool> HasStoredCredentials()
        {
            return true;
        }

        public async Task<bool> TryRefresh()
        {
            return true;
        }
    }
}
