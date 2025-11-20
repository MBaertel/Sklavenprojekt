using Microsoft.AspNetCore.Mvc;
using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers.GetControllers
{
    [Route("api/get/[controller]")]
    [ApiController]
    public class GetController(SchoolManagementContext context) : ControllerBase
    {
        [HttpGet("GetTeacher")]
        public IActionResult GetTeacher(Teacher teacher)
        {
            var teacherEf = context.Teachers.Single(x => x.Id == teacher.Id);
            return Ok(teacherEf);
        }

        [HttpGet("GetTeachers")]
        public IActionResult GetTeachers(List<Teacher> teacherList)
        {
            var teachers = new List<EFTeacher>();
            foreach (var teacher in teacherList)
            {
                var teacherEf = context.Teachers.Where(x => x.Id == teacher.Id).ToList();
                teachers.AddRange(teacherEf);
            }
            return Ok(teachers);
        }
        
        [HttpGet("GetSubject")]
        public IActionResult GetSubject(Subject subject)
        {
            var subjectEf = context.Subjects.Single(x => x.Id == subject.Id);
            return Ok(subjectEf);
        }

        [HttpGet("GetSubjects")]
        public IActionResult GetSubjects(List<Subject> subjectList)
        {
            var efSubjects = new List<EFSubject>();
            foreach (var subject in subjectList)
            {
                var foundEfSubjects = context.Subjects.Where(x => x.Id == subject.Id).ToList();
                efSubjects.AddRange(foundEfSubjects);
            }
            return Ok(efSubjects);
        }
        
        [HttpGet("GetStudent")]
        public IActionResult GetStudent(Student student)
        {
            var efStudent = context.Students.Single(x => x.Id == student.Id);
            return Ok(efStudent);
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudents(List<Student> studentList)
        {
            var efStudents = new List<EFStudent>();
            foreach (var student in studentList)
            {
                var foundEfStudents = context.Students.Where(x => x.Id == student.Id).ToList();
                efStudents.AddRange(foundEfStudents);
            }
            return Ok(efStudents);
        }
        
        [HttpGet("GetClass")]
        public IActionResult GetClass(Class @class)
        {
            var efClass = context.Class.Single(x => x.Id == @class.Id);
            return Ok(efClass);
        }

        [HttpGet("GetClasses")]
        public IActionResult GetClasses(List<Class> classList)
        {
            var efClasses = new List<EFClass>();
            foreach (var @class in classList)
            {
                var found = context.Class.Where(x => x.Id == @class.Id).ToList();
                efClasses.AddRange(found);
            }
            return Ok(efClasses);
        }

        // EXAMS
        [HttpGet("GetExam")]
        public IActionResult GetExam(ClassExam exam)
        {
            var efClassExam = context.ClassExams.Single(x => x.Id == exam.Id);
            return Ok(efClassExam);
        }

        [HttpGet("GetExams")]
        public IActionResult GetExams(List<ClassExam> examList)
        {
            var efExams = new List<EFClassExam>();
            foreach (var exam in examList)
            {
                var found = context.ClassExams.Where(x => x.Id == exam.Id).ToList();
                efExams.AddRange(found);
            }
            return Ok(efExams);
        }

        [HttpGet("GetIndividualExam")]
        public IActionResult GetIndividualExam(IndividualExam exam)
        {
            var ef = context.IndividualExams.Single(x => x.Id == exam.Id);
            return Ok(ef);
        }

        [HttpGet("GetIndividualExams")]
        public IActionResult GetIndividualExams(List<IndividualExam> exams)
        {
            var list = new List<EFIndividualExam>();
            foreach (var exam in exams)
            {
                var found = context.IndividualExams.Where(x => x.Id == exam.Id).ToList();
                list.AddRange(found);
            }
            return Ok(list);
        }
        
        [HttpGet("GetExamImage")]
        public IActionResult GetExamImage(ExamImages image)
        {
            var ef = context.ExamImages.Single(x => x.Id == image.Id);
            return Ok(ef);
        }

        [HttpGet("GetExamImages")]
        public IActionResult GetExamImages(List<ExamImages> images)
        {
            var list = new List<EFExamImage>();
            foreach (var image in images)
            {
                var found = context.ExamImages.Where(x => x.Id == image.Id).ToList();
                list.AddRange(found);
            }
            return Ok(list);
        }
    }
}
