using SchoolManagementFrontend.ViewModels.MainPages.StudentsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Pages
{
    public class StudentsPageDescriptor : PageDescriptor<StudentsPageViewModel>
    {
        private static string ICON_PATH = "/Assets/Icons/student.svg";

        private static List<string> PERMISSIONS = new List<string>
        {
            "students.read"
        };

        public StudentsPageDescriptor()
            : base("Students", ICON_PATH)
        {
        }
    }
}
