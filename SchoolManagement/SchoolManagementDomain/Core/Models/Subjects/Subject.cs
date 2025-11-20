namespace SchoolManagementDomain.Core.Models.Subjects;

public class Subject
{
    public Guid Id { get; set; }
    public Guid BaseSubjectId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid ClassId { get; set; }
}