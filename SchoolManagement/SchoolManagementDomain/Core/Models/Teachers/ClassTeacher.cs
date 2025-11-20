using SchoolManagementDomain.Core.Models.Classes;

namespace SchoolManagementDomain.Core.Models.Teachers;

public class ClassTeacher
{
    public Teacher Teacher { get; set; }
    public Class Class { get; set; }
}