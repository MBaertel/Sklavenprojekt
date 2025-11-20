namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFTeacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<EFClassTeacher> ClassTeachers { get; set; }
        public IEnumerable<EFClass> Classes => ClassTeachers.Select(ct => ct.Class);

        public ICollection<EFSubject> Subjects {  get; set; }
    }
}
