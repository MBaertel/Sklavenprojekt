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

namespace SchoolManagementFrontend.Services.Mock
{
    public static class MockDataProvider
    {
        public static List<Teacher> Teachers = new List<Teacher>()
        {
            new Teacher() { Id = Guid.NewGuid(), Name = "Thomas Menne" },
            new Teacher() { Id = Guid.NewGuid(), Name = "Anna Schmidt" },
            new Teacher() { Id = Guid.NewGuid(), Name = "Lukas Fischer" },
        };

        public static List<Student> Students = new List<Student>()
        {
            new Student() { Id = Guid.NewGuid(), Name = "Maximilian Bärtel" },
            new Student() { Id = Guid.NewGuid(), Name = "Laura Klein" },
            new Student() { Id = Guid.NewGuid(), Name = "Jonas Weber" },
            new Student() { Id = Guid.NewGuid(), Name = "Sophia Mayer" },
            new Student() { Id = Guid.NewGuid(), Name = "Felix Hoffmann" },
        };

        public static List<BaseSubject> BaseSubjects = new List<BaseSubject>()
        {
            new BaseSubject() { Id = Guid.NewGuid(), Name = "Mathematics" },
            new BaseSubject() { Id = Guid.NewGuid(), Name = "Physics" },
            new BaseSubject() { Id = Guid.NewGuid(), Name = "Computer Science" },
            new BaseSubject() { Id = Guid.NewGuid(), Name = "History" },
            new BaseSubject() { Id = Guid.NewGuid(), Name = "SUD" },
        };

        public static List<Class> Classes = new List<Class>()
        {
            new Class() { Id = Guid.NewGuid(), Name = "ITB1-24" },
            new Class() { Id = Guid.NewGuid(), Name = "ITB2-24" },
            new Class() { Id = Guid.NewGuid(), Name = "ITB3-24" },
        };

        public static List<ClassTeacher> ClassTeachers = new List<ClassTeacher>()
        {
            new ClassTeacher() { Class = Classes[0], Teacher = Teachers[0] },
            new ClassTeacher() { Class = Classes[1], Teacher = Teachers[1] },
            new ClassTeacher() { Class = Classes[2], Teacher = Teachers[2] },
        };

        public static List<ClassStudents> ClassStudents = new List<ClassStudents>()
        {
            new ClassStudents() { Class = Classes[0], Student = Students[0] },
            new ClassStudents() { Class = Classes[0], Student = Students[1] },
            new ClassStudents() { Class = Classes[1], Student = Students[2] },
            new ClassStudents() { Class = Classes[1], Student = Students[3] },
            new ClassStudents() { Class = Classes[2], Student = Students[4] },
            new ClassStudents() { Class = Classes[2], Student = Students[0] },
        };

        public static List<Subject> Subjects = new List<Subject>()
        {
            new Subject() { Id = Guid.NewGuid(), BaseSubject = BaseSubjects[0], Class = Classes[0], Teacher = Teachers[0] },
            new Subject() { Id = Guid.NewGuid(), BaseSubject = BaseSubjects[1], Class = Classes[0], Teacher = Teachers[1] },
            new Subject() { Id = Guid.NewGuid(), BaseSubject = BaseSubjects[2], Class = Classes[1], Teacher = Teachers[2] },
            new Subject() { Id = Guid.NewGuid(), BaseSubject = BaseSubjects[3], Class = Classes[1], Teacher = Teachers[1] },
            new Subject() { Id = Guid.NewGuid(), BaseSubject = BaseSubjects[4], Class = Classes[2], Teacher = Teachers[0] },
        };

        public static List<SubjectStudent> SubjectStudents = new List<SubjectStudent>()
        {
            new SubjectStudent() { Student = Students[0], Subject = Subjects[0] },
            new SubjectStudent() { Student = Students[1], Subject = Subjects[0] },
            new SubjectStudent() { Student = Students[2], Subject = Subjects[2] },
            new SubjectStudent() { Student = Students[3], Subject = Subjects[3] },
            new SubjectStudent() { Student = Students[4], Subject = Subjects[4] },
            new SubjectStudent() { Student = Students[0], Subject = Subjects[4] },
        };

        public static List<ClassExam> ClassExams = new List<ClassExam>()
        {
            new ClassExam() { Id = Guid.NewGuid(), Class = Classes[0], Subject = Subjects[0], Name = "Mathematics Exam 1" },
            new ClassExam() { Id = Guid.NewGuid(), Class = Classes[0], Subject = Subjects[1], Name = "Physics Exam 1" },
            new ClassExam() { Id = Guid.NewGuid(), Class = Classes[1], Subject = Subjects[2], Name = "CS Exam 1" },
            new ClassExam() { Id = Guid.NewGuid(), Class = Classes[1], Subject = Subjects[3], Name = "History Exam 1" },
            new ClassExam() { Id = Guid.NewGuid(), Class = Classes[2], Subject = Subjects[4], Name = "SUD Exam 1" },
        };

        public static List<IndividualExam> IndividualExams = new List<IndividualExam>()
        {
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[0], Student = Students[0] },
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[0], Student = Students[1] },
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[2], Student = Students[2] },
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[3], Student = Students[3] },
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[4], Student = Students[4] },
            new IndividualExam() { Id = Guid.NewGuid(), BaseExam = ClassExams[4], Student = Students[0] },
        };
    }
}
