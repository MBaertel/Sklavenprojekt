namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClassExam
    {
        public Guid Id { get; set; }
        public EFClass Class { get; set; }
        public Guid ClassId { get; set; }

        public EFSubject Subject { get; set; }
        public Guid SubjectId { get; set; }

        public DateTime Date { get; set; }


        public ICollection<EFIndividualExam> IndividualExams { get; set; }
    }
}
