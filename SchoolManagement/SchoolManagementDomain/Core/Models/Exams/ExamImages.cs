namespace SchoolManagementDomain.Core.Models.Exams;

public class ExamImages
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}