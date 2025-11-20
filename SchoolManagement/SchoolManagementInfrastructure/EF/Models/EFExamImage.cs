namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFExamImage
    {
        public Guid Id { get; set; }
        public EFIndividualExam ExamId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
