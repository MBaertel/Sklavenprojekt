using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Mock
{
    internal class MockBackendService : IBackendService
    {
        private List<Class> classes = MockDataProvider.Classes;
        private List<Student> students = MockDataProvider.Students;
        private List<Teacher> teachers = MockDataProvider.Teachers;
        private List<Subject> subjects = MockDataProvider.Subjects;
        private List<ClassExam> classExams = MockDataProvider.ClassExams;
        private List<IndividualExam> individualExams = MockDataProvider.IndividualExams;
        private List<ClassStudents> classStudents = MockDataProvider.ClassStudents;
        private List<ClassTeacher> classTeachers = MockDataProvider.ClassTeachers;
        private List<SubjectStudent> subjectStudents = MockDataProvider.SubjectStudents;
        private List<ExamImages> examImages = new();

        public event Action NewObject;

        public Task<Class> GetClass(Guid id) =>
            Task.FromResult(classes.Where(x => x.Id == id).FirstOrDefault());

        public Task<ClassExam> GetClassExam(Guid id) =>
            Task.FromResult(classExams.Where(x => x.Id == id).FirstOrDefault());

        public Task<IndividualExam> GetIndividualExam(Guid id) =>
            Task.FromResult(individualExams.Where(x => x.Id == id).FirstOrDefault());

        public Task<Student> GetStudent(Guid id) =>
            Task.FromResult(students.Where(x => x.Id == id).FirstOrDefault());

        public Task<Subject> GetSubject(Guid id) =>
            Task.FromResult(subjects.Where(x => x.Id == id).FirstOrDefault());

        public Task<Teacher> GetTeacher(Guid id) =>
            Task.FromResult(teachers.Where(x => x.Id == id).FirstOrDefault());


        public Task<List<Class>> GetClasses(Guid? studentId = null, Guid? teacherId = null)
        {
            if (studentId.HasValue)
                return Task.FromResult(classStudents
                    .Where(x => x.Student.Id == studentId.Value)
                    .Select(x => x.Class)
                    .ToList());
            if (teacherId.HasValue)
                return Task.FromResult(classTeachers
                    .Where(x => x.Teacher.Id == teacherId.Value)
                    .Select(x => x.Class)
                    .Concat(subjects
                        .Where(x => x.Teacher.Id == teacherId.Value)
                        .Select(x => x.Class))
                    .ToList());
            return Task.FromResult(classes.ToList());
        }

        public Task<List<ClassExam>> GetClassExams(Guid? classId = null, Guid? subjectId = null, Guid? teacherId = null)
        {
            if (classId.HasValue)
                return Task.FromResult(classExams
                    .Where(x => x.Class.Id == classId.Value)
                    .ToList());
            if (subjectId.HasValue)
                return Task.FromResult(classExams
                    .Where(x => x.Subject.Id == subjectId.Value)
                    .ToList());
            if (teacherId.HasValue)
                return Task.FromResult(classExams
                    .Where(x => x.Subject.Teacher.Id == teacherId.Value)
                    .ToList());
            return Task.FromResult(classExams.ToList());
        }

        public Task CreateClassExam(ClassExam exam)
        {
            classExams.Add(exam);
            var students = subjectStudents.Where(x => x.Subject.Id == exam.Subject.Id).Select(x => x.Student);
            var newIndividualExams = students.Select(x => new IndividualExam()
            {
                Student = x,
                BaseExam = exam,
                Id = Guid.NewGuid(),
            });
            individualExams.AddRange(newIndividualExams);

            NewObject?.Invoke();
            return Task.CompletedTask;
        }

        public Task UpdateClassExam(Guid id,ClassExam exam)
        {
            var prevExam = classExams.FirstOrDefault(x => x.Id == id);
            if (prevExam == null) return Task.CompletedTask;

            prevExam.Name = exam.Name;
            prevExam.Date = exam.Date;

            NewObject?.Invoke();
            return Task.CompletedTask;
        }

        public Task<List<IndividualExam>> GetIndividualExams(Guid? classExamId = null, Guid? studentId = null)
        {
            if (classExamId.HasValue)
                return Task.FromResult(individualExams
                    .Where(x => x.BaseExam.Id == classExamId)
                    .ToList());
            if (studentId.HasValue)
                return Task.FromResult(individualExams
                    .Where(x => x.Student.Id == studentId)
                    .ToList());
            return Task.FromResult(individualExams.ToList());
        }

        public Task<List<Student>> GetStudents(Guid? classId = null, Guid? subjectId = null, Guid? classExamId = null)
        {
            if (classId.HasValue)
                return Task.FromResult(classStudents
                    .Where(x => x.Class.Id == classId)
                    .Select(x => x.Student)
                    .ToList());
            if (subjectId.HasValue)
                return Task.FromResult(subjectStudents
                    .Where(x => x.Subject.Id == subjectId)
                    .Select(x => x.Student)
                    .ToList());
            if (classExamId.HasValue)
                return Task.FromResult(individualExams
                    .Where(x => x.BaseExam.Id == classExamId)
                    .Select(x => x.Student)
                    .ToList());
            return Task.FromResult(students.ToList());
        }

        public Task<List<Subject>> GetSubjects(Guid? studentId = null, Guid? teacherId = null)
        {
            if (studentId.HasValue)
                return Task.FromResult(subjectStudents
                    .Where(x => x.Student.Id == studentId)
                    .Select(x => x.Subject)
                    .ToList());
            if (teacherId.HasValue)
                return Task.FromResult(subjects
                    .Where(x => x.Teacher.Id == teacherId)
                    .ToList());
            return Task.FromResult(subjects.ToList());
        }

        public Task<List<Teacher>> GetTeachers()
        {
            return Task.FromResult(teachers.ToList());
        }

        public Task<List<ExamImages>> GetExamImages(Guid individualExamId)
        {
            return Task.FromResult(examImages.Where(x => x.Exam.Id == individualExamId).ToList());
        }

        public Task UploadExamImage(ExamImages examImage, byte[] imageData)
        {
            
            examImage.Id = Guid.NewGuid();
            if(!Directory.Exists("./ImageUploads"))
            {
                Directory.CreateDirectory("./ImageUploads");
            }
            File.WriteAllBytes($"./ImageUploads/{examImage.Id}.jpg", imageData);
            examImage.Link = $"./ImageUploads/{examImage.Id}.jpg";
            
            examImages.Add(examImage);
            NewObject?.Invoke();
            return Task.CompletedTask;
        }

        public Task UpdateIndividualExam(Guid id, IndividualExam exam)
        {
            var prevExam = individualExams.FirstOrDefault(x => x.Id == id);
            prevExam.Finalized = exam.Finalized;
            prevExam.Score = exam.Score;
            prevExam.Grade = exam.Grade;
            prevExam.Notes = exam.Notes;
            prevExam.HandedInAt = exam.HandedInAt;

            NewObject?.Invoke();
            return Task.CompletedTask;
        }
    }
}
