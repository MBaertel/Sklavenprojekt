using SchoolManagementDomain.Core.Models.Classes;

namespace SchoolManagementDomain.Core.Models.Students;

public class ClassStudents
{
    public Student Student { get; set; }
    public Class Class { get; set; }
}