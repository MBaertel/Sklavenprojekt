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
            new Teacher() {Id = Guid.Parse("806d66ae-8dee-4b99-905c-63426909ba81"), Name= "Frau Albers"},
            new Teacher() {Id = Guid.Parse("16d60e87-d96f-47fd-a725-d5ef2f39b258"), Name= "Heinrich Hecker"},
            new Teacher() {Id = Guid.Parse("6bb62469-263e-4425-9f07-c24d8cbf0e12"), Name= "Sven Deitmer"},
            new Teacher() {Id = Guid.Parse("0bcfcd22-493b-47b0-b115-6bc806684da7"), Name= "Herr Kröger"}
        };

        public static List<Student> Students = new List<Student>()
        {
            new Student() {Id = Guid.Parse("949a9ef7-6239-465b-889e-1d8e7d2460fa"), Name= "Maximilian Bärtel"},
            new Student() {Id = Guid.Parse("b4e20491-1ca8-4447-9063-5053a27eead3"), Name= "Leon Hater"},
        };

        public static List<BaseSubject> BaseSubjects = new List<BaseSubject>()
        {
            new BaseSubject() {Id=Guid.Parse("d1a538ab-50e9-45e3-a918-d2310dcb1fc4"), Name= "EVP"},
            new BaseSubject() {Id=Guid.Parse("5f1b2ab0-d923-4bbe-b338-b45203163ddc"), Name= "SUD"},
            new BaseSubject() {Id=Guid.Parse("ba84b057-4cf7-42d7-ac16-0c3c3faddb6a"), Name= "WUB"}
        };

        public static List<Class> Classes = new List<Class>()
        {
            new Class() {Id=Guid.Parse("fc3ec1f0-2c58-4a64-8618-d5443a9f502a"),Name="ITB1-24"},
            new Class() {Id=Guid.Parse("44f6a0c3-3952-49c1-9daa-39381b06e676"),Name="ITB2-24"}
        };

        public static List<ClassTeacher> ClassTeachers = new List<ClassTeacher>()
        {
            new ClassTeacher() {Class=Classes[0],Teacher=Teachers[4] },
            new ClassTeacher() {Class=Classes[1],Teacher=Teachers[0] },
        };

        public static List<ClassStudents> ClassStudents = new List<ClassStudents>()
        {
            new ClassStudents() {Class= Classes[0],Student= Students[0]},
            new ClassStudents() {Class= Classes[0],Student= Students[1]},
        };

        public static List<Subject> Subjects = new List<Subject>()
        {
            new Subject() {Id = Guid.Parse("6db712df-3d67-46ba-adde-a9b2eaac1668"),BaseSubject= BaseSubjects[1],Class=Classes[0],Teacher=Teachers[0]},
            new Subject() {Id = Guid.Parse("9973a33b-26af-44c7-bf0a-9c6820dc29bb"),BaseSubject= BaseSubjects[0],Class=Classes[0],Teacher=Teachers[2]},
            new Subject() {Id = Guid.Parse("271ee836-945c-4a42-8e5f-2fec8ce45e76"),BaseSubject= BaseSubjects[2],Class=Classes[0],Teacher=Teachers[3]}
        };

        public static List<SubjectStudent> SubjectStudents = new List<SubjectStudent>()
        {
            new SubjectStudent() {Student = Students[0],Subject= Subjects[0]},
            new SubjectStudent() {Student = Students[1],Subject= Subjects[0]},
            new SubjectStudent() {Student = Students[0],Subject= Subjects[1]},
            new SubjectStudent() {Student = Students[1],Subject= Subjects[1]}
        };

        public static List<ClassExam> ClassExams = new List<ClassExam>()
        {
            new ClassExam() {Id = Guid.Parse("eaeff41e-bc57-4922-9464-e129c3bd6ba4"),Class=Classes[0],Subject=Subjects[0],Name="SUD Klausur 1"},
            new ClassExam() {Id = Guid.Parse("3fbd612b-7bc7-4c50-948a-0f3dbc5f79ca"),Class=Classes[0],Subject=Subjects[1],Name="EVP Klausur 1"}
        };

        public static List<IndividualExam> IndividualExams = new List<IndividualExam>()
        {
            new IndividualExam() {Id = Guid.Parse("2f505bcc-bd11-4dba-ac84-68f266b3c043"),BaseExam = ClassExams[0],Student=Students[0]},
            new IndividualExam() {Id = Guid.Parse("2f505bcc-bd11-4dba-ac84-68f266b3c043"),BaseExam = ClassExams[0],Student=Students[1]},
            new IndividualExam() {Id = Guid.Parse("d9b144cc-082f-4d77-812c-3d56b7481336"),BaseExam = ClassExams[1],Student=Students[0]},
            new IndividualExam() {Id = Guid.Parse("ea7f20cc-ec4f-4a0f-a4b6-728aaba8a64d"),BaseExam = ClassExams[1],Student=Students[1]},
        };


    }
}
