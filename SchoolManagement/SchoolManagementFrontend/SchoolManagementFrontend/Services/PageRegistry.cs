using SchoolManagementFrontend.Pages;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services
{
    internal class PageRegistry : IPageRegistry
    {
        private readonly IAuthService _authService;

        private HashSet<IPageDescriptor> _allPages;
        public IReadOnlyList<IPageDescriptor> Pages => _allPages
            .Where(p => _authService.HasAllRoles(p.RequiredRoles))
            .ToList()
            .AsReadOnly();

        public event EventHandler PagesUpdated;

        public PageRegistry(IAuthService authService)
        {
            _authService = authService;
            _authService.UserLoggedIn += (s, e) => RefreshPages();
        }

        public void RegisterPage(IPageDescriptor page)
        {
            _allPages.Add(page);
        }

        private void RefreshPages()
        {
            PagesUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
