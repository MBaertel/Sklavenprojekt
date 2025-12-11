using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels.MainPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Pages
{
    public class ExamsPageDescriptor : PageDescriptor<ExamsPageViewModel>
    {
        private static string ICON_PATH = "/Assets/Icons/exam.svg";

        private static List<string> PERMISSIONS = new List<string>
        {
            "exams.read"
        };
        
        public ExamsPageDescriptor(IPageRegistry pageRegistry,ExamsPageViewModel vm)
            :base("Exams",ICON_PATH,vm)
        {
        }
    }
}
