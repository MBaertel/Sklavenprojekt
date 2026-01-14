using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementDomain.Core.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface IBackendService
    {
        public event Action NewObject;
        public Task<List<Teacher>> GetTeachers();
        public Task<Teacher> GetTeacher(Guid id);

        public Task<List<ClassExam>> GetClassExams(Guid? classId = null, Guid? subjectId = null, Guid? teacherId = null);
        public Task<ClassExam> GetClassExam(Guid id);
        public Task UpdateClassExam(Guid id,ClassExam exam);

        public Task CreateClassExam(ClassExam classExam);

        public Task<List<Student>> GetStudents(Guid? classId = null, Guid? subjectId = null, Guid? classExamId = null);
        public Task<Student> GetStudent(Guid id);

        public Task<List<IndividualExam>> GetIndividualExams(Guid? classExamId = null, Guid? studentId = null);
        public Task<IndividualExam> GetIndividualExam(Guid id);
        public Task UpdateIndividualExam(Guid id, IndividualExam exam);

        public Task<List<Class>> GetClasses(Guid? studentId = null, Guid? teacherId = null);
        public Task<Class> GetClass(Guid id);

        public Task<List<Subject>> GetSubjects(Guid? studentId = null, Guid? teacherId = null);
        public Task<Subject> GetSubject(Guid id);
        public Task<List<ExamImages>> GetExamImages(Guid individualExamId);
        public Task UploadExamImage(ExamImages examImages, byte[] imageData);
    }
}
