namespace SchoolManagementDomain.Core.Models.Teachers;

public class Teacher
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ClassTeacher> ClassTeachers { get; set; }
}