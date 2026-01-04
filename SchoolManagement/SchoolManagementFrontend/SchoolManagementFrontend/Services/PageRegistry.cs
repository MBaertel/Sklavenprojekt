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
        private HashSet<IPageDescriptor> _allPages;
        public IReadOnlyList<IPageDescriptor> Pages => _allPages
            .ToList()
            .AsReadOnly();

        public event EventHandler PagesUpdated;

        public PageRegistry()
        {
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
