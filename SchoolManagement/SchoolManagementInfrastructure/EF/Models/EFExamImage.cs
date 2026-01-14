namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFExamImage
    {
        public Guid Id { get; set; }
        public EFIndividualExam Exam { get; set; }
        public Guid ExamId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public byte[] ImageData { get; set; }
    }
}
