namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFStudent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<EFClassStudent> ClassStudents { get; set; }
        public IEnumerable<EFClass> Classes => ClassStudents.Select(e => e.Class);

        public ICollection<EFSubjectStudent> SubjectStudents { get; set; }
        public IEnumerable<EFSubject> Subjects => SubjectStudents.Select(e => e.Subject);


        public ICollection<EFIndividualExam> IndividualExams { get; set; }
    }
}


