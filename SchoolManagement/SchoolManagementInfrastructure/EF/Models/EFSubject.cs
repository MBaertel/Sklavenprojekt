namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFSubject
    {
        public Guid Id { get; set; }

        public EFBaseSubject BaseSubject { get; set; }
        public Guid BaseSubjectId { get; set; }

        public EFTeacher Teacher { get; set; }
        public Guid TeacherId { get; set; }

        public EFClass Class { get; set; }
        public Guid ClassId { get; set; }


        public ICollection<EFSubjectStudent> SubjectStudents { get; set; }
        public IEnumerable<EFStudent> Students => SubjectStudents.Select(x => x.Student);

        
        public ICollection<EFClassExam> ClassExams { get; set; }
    }
}
