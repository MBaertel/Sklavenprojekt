using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.OutputModels
{
    public class TeacherDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Classes { get; set; }
        public List<Guid> Subjects { get; set; }

        public static TeacherDTO FromEf(EFTeacher efTeacher)
        {
            var dto = new TeacherDTO()
            {
                Id = efTeacher.Id,
                Name = efTeacher.Name,
            };
            dto.Classes = efTeacher.ClassTeachers.Select(x => x.ClassId).ToList();
            dto.Subjects = efTeacher.Subjects.Select(x => x.Id).ToList();

            if (dto.Classes.Count == 0) dto.Classes = null;
            if (dto.Subjects.Count == 0) dto.Subjects = null;

            return dto;
        }
    }
}
