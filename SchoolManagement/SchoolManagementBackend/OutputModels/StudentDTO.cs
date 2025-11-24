using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.OutputModels
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Guid> Classes { get; set; }
        public List<Guid> Subjects { get; set; }
        public List<Guid> Exams { get; set; }

        public static StudentDTO FromEf(EFStudent efStudent)
        {
            var dto = new StudentDTO
            {
                Id = efStudent.Id,
                Name = efStudent.Name,
            };
            dto.Classes = efStudent.ClassStudents.Select(x => x.ClassId).ToList();
            dto.Subjects = efStudent.SubjectStudents.Select(x => x.SubjectId).ToList();
            dto.Exams = efStudent.IndividualExams.Select(x => x.Id).ToList();

            if (dto.Classes.Count == 0) dto.Classes = null;
            if (dto.Subjects.Count == 0) dto.Subjects = null;
            if (dto.Exams.Count == 0) dto.Exams = null;

            return dto;
        }
    }
}
