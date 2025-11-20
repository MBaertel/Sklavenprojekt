namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFBaseSubject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<EFSubject> Subjects { get; set; }
    }
}
