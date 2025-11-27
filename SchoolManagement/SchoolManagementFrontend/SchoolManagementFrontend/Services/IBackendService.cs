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

namespace SchoolManagementFrontend.Services
{
    public interface IBackendService
    {
        public List<Teacher> GetTeachers();
        public Teacher GetTeacher(Guid id);

        public List<ClassExam> GetClassExams(Guid? classId = null, Guid? subjectId = null, Guid? teacherId = null);
        public ClassExam GetClassExam(Guid id);

        public List<Student> GetStudents(Guid? classId = null,Guid? subjectId = null,Guid? classExamId = null);
        public Student GetStudent(Guid id);

        public List<IndividualExam> GetIndividualExams(Guid? classExamId = null, Guid? studentId = null);
        public IndividualExam GetIndividualExam(Guid id);

        public List<Class> GetClasses(Guid? studentId = null, Guid? teacherId = null);
        public Class GetClass(Guid id);

        public List<Subject> GetSubjects(Guid? studentId = null, Guid? teacherId = null);

        public Subject GetSubject(Guid id);
    }
}
