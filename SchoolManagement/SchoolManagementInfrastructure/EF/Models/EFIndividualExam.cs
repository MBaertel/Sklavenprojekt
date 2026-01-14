namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFIndividualExam
    {
        public Guid Id { get; set; }
        public EFClassExam BaseExam { get; set; }
        public Guid BaseExamId { get; set; }
        public int? Score { get; set; }
        public EFStudent Student { get; set; }
        public Guid StudentId { get; set; }
        public string Grade { get; set; }
        public DateTime HandedInAt { get; set; }
        public string Notes { get; set; }
        public bool Finalized { get; set; } = false;
        public ICollection<EFExamImage> Images { get; set; }
    }
}
