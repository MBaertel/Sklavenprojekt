using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.OutputModels
{
    public class ClassDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static ClassDTO FromEf(EFClass efclass)
        {
            return new ClassDTO
            {
                Id = efclass.Id,
                Name = efclass.Name,
            };
        }

    }
}
