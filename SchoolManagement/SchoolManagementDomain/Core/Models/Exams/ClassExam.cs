namespace SchoolManagementDomain.Core.Models.Exams;

public class ClassExam
{
    public Guid Id { get; set; }
    public Guid ClassId { get; set; }
    public Guid SubjectId { get; set; }
    public DateTime Date { get; set; }
}