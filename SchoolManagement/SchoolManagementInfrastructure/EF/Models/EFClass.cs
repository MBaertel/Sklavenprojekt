using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EFClassStudent> ClassStudents { get; set; }
        public IEnumerable<EFStudent> Students => ClassStudents.Select(x => x.Student);

        public ICollection<EFClassTeacher> ClassTeachers { get; set; }
        public ICollection<EFSubject> Subjects { get; set; }
        public ICollection<EFClassExam> ClassExams { get; set; }
    }
}
