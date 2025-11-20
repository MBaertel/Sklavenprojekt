using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Subjects;

namespace SchoolManagementDomain.Core.Models.Exams;

public class ClassExam
{
    public Guid Id { get; set; }
    public Class Class { get; set; }
    public Subject Subject { get; set; }
    public DateTime Date { get; set; }
}