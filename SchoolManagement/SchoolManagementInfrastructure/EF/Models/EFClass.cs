namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<EFClassStudent> ClassStudents { get; set; }
        public IEnumerable<EFStudent> Students => ClassStudents.Select(x => x.Student);

        public ICollection<EFClassTeacher> ClassTeachers { get; set; }
        public ICollection<EFSubject> Subjects { get; set; }
        public ICollection<EFClassExam> ClassExams { get; set; }
    }
}
