namespace SchoolManagementDomain.Core.Models.Subjects;

public class SubjectStudent
{
    public Guid SubjectId { get; set; }
    public Guid StudentId { get; set; }
    public int Grade { get; set; }
}