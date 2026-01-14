using SchoolManagementDomain.Core.Models.Students;

namespace SchoolManagementDomain.Core.Models.Exams
{
    public class IndividualExam
    {
        public Guid Id { get; set; }
        public ClassExam BaseExam { get; set; }
        public int Score { get; set; }
        public Student Student { get; set; }
        public string Grade { get; set; }
        public DateTime HandedInAt { get; set; }
        public bool Finalized { get; set; }
    }
}
