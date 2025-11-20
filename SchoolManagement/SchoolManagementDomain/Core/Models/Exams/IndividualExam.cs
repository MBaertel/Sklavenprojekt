namespace SchoolManagementDomain.Core.Models.Exams
{
    public class IndividualExam
    {
        public Guid Id { get; set; }
        public Guid BaseExam { get; set; }
        public int Score { get; set; }
        public Guid Student { get; set; }
        public int Grade { get; set; }
    }
}
