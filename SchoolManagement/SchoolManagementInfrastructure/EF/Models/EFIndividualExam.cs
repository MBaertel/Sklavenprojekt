using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFIndividualExam
    {
        public Guid Id { get; set; }
        public EFClassExam BaseExam { get; set; }
        public Guid BaseExamId { get; set; }

        public int Score { get; set; }

        public EFStudent Student { get; set; }
        public Guid StudentId { get; set; }

        public int Grade { get; set; }

        public ICollection<EFExamImage> Images { get; set; }
    }
}
