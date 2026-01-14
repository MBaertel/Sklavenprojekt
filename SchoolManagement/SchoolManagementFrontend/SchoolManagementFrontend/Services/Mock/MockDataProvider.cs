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
            new Teacher() {Id = Guid.Parse("95744a52-7107-49be-836e-72505ff04e05"), Name= "Thomas Menne"},
        };

        public static List<Student> Students = new List<Student>()
        {
            new Student() {Id = Guid.Parse("949a9ef7-6239-465b-889e-1d8e7d2460fa"), Name= "Maximilian Bärtel"},
        };

        public static List<BaseSubject> BaseSubjects = new List<BaseSubject>()
        {
            new BaseSubject() {Id=Guid.Parse("d1a538ab-50e9-45e3-a918-d2310dcb1fc4"), Name= "SUD"},
        };

        public static List<Class> Classes = new List<Class>()
        {
            new Class() {Id=Guid.Parse("fc3ec1f0-2c58-4a64-8618-d5443a9f502a"),Name="ITB1-24"},
            new Class() {Id=Guid.Parse("44f6a0c3-3952-49c1-9daa-39381b06e676"),Name="ITB2-24"}
        };

        public static List<ClassTeacher> ClassTeachers = new List<ClassTeacher>()
        {
        };

        public static List<ClassStudents> ClassStudents = new List<ClassStudents>()
        {
            new ClassStudents() {Class= Classes[0],Student= Students[0]},
            new ClassStudents() {Class= Classes[1],Student= Students[0]},
        };

        public static List<Subject> Subjects = new List<Subject>()
        {
            new Subject() {Id = Guid.Parse("6db712df-3d67-46ba-adde-a9b2eaac1668"),BaseSubject= BaseSubjects[0],Class=Classes[0],Teacher=Teachers[0]},
            new Subject() {Id = Guid.Parse("9973a33b-26af-44c7-bf0a-9c6820dc29bb"),BaseSubject= BaseSubjects[0],Class=Classes[1],Teacher=Teachers[0]},
        };

        public static List<SubjectStudent> SubjectStudents = new List<SubjectStudent>()
        {
            new SubjectStudent() {Student = Students[0],Subject= Subjects[0]},
            new SubjectStudent() {Student = Students[0],Subject= Subjects[1]},
        };

        public static List<ClassExam> ClassExams = new List<ClassExam>()
        {
            new ClassExam() {Id = Guid.Parse("eaeff41e-bc57-4922-9464-e129c3bd6ba4"),Class=Classes[0],Subject=Subjects[0],Name="SUD Klausur 1"},
            new ClassExam() {Id = Guid.Parse("3fbd612b-7bc7-4c50-948a-0f3dbc5f79ca"),Class=Classes[1],Subject=Subjects[1],Name="EVP Klausur 2"},
        };

        public static List<IndividualExam> IndividualExams = new List<IndividualExam>()
        {
            new IndividualExam() {Id = Guid.Parse("156d816e-2246-4e17-a106-41a8884bdb75"),BaseExam = ClassExams[0],Student=Students[0]},
            new IndividualExam() {Id = Guid.Parse("307dc171-ca7a-4d33-b110-7e93306f898d"),BaseExam = ClassExams[1],Student=Students[0]},
        };


    }
}
