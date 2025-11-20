using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Teachers;

namespace SchoolManagementDomain.Core.Models.Subjects;

public class Subject
{
    public Guid Id { get; set; }
    public BaseSubject BaseSubject { get; set; }
    public Teacher Teacher { get; set; }
    public Class Class { get; set; }
}