using SchoolManagementDomain.Core.Models.Students;

namespace SchoolManagementDomain.Core.Models.Subjects;

public class SubjectStudent
{
    public Subject Subject { get; set; }
    public Student Student { get; set; }
    public int Grade { get; set; }
}