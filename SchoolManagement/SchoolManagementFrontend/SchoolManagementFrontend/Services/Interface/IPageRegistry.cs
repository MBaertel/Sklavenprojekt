using SchoolManagementFrontend.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface IPageRegistry
    {
        IReadOnlyList<IPageDescriptor> Pages { get; }

        event EventHandler<IPageDescriptor> PagesUpdated;
        public void RegisterPage(IPageDescriptor page);
    }
}
