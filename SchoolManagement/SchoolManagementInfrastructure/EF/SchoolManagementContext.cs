using Microsoft.EntityFrameworkCore;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementInfrastructure.EF
{
    public class SchoolManagementContext : DbContext
    {
        public DbSet<EFStudent> Students { get; set; }
        public DbSet<EFTeacher> Teachers { get; set; }
        public DbSet<EFClassTeacher> ClassTeachers { get; set; }
        public DbSet<EFBaseSubject> BaseSubjects { get; set; }
        public DbSet<EFSubject> Subjects { get; set; }
        public DbSet<EFClass> Class { get; set; }

        public DbSet<EFClassStudent> ClassStudents { get; set; }
        public DbSet<EFIndividualExam> IndividualExams { get; set; }
        public DbSet<EFSubjectStudent> SubjectStudents { get; set; }
        public DbSet<EFClassExam> ClassExams { get; set; }
        public DbSet<EFExamImage> ExamImages { get; set; }
        public DbSet<EFUser> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=project;Username=admin;Password=adminpass");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EFStudent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EFClass>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EFTeacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EFBaseSubject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<EFSubject>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.Subjects)
                    .HasForeignKey(e => e.TeacherId);

                entity.HasOne(e => e.Class)
                    .WithMany(c => c.Subjects)
                    .HasForeignKey(e => e.ClassId);

                entity.HasOne(e => e.BaseSubject)
                    .WithMany(b => b.Subjects)
                    .HasForeignKey(e => e.BaseSubjectId);
            });

            modelBuilder.Entity<EFClassTeacher>(entity =>
                {
                    entity.HasKey(e => new { e.ClassId, e.TeacherId });

                    entity.HasOne(e => e.Class)
                        .WithMany(c => c.ClassTeachers)
                        .HasForeignKey(e => e.ClassId);
                    
                    entity.HasOne(e => e.Teacher)
                        .WithMany(c => c.ClassTeachers)
                        .HasForeignKey(e => e.TeacherId);
                }
            );

            modelBuilder.Entity<EFClassExam>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Class)
                    .WithMany(c => c.ClassExams)
                    .HasForeignKey(e => e.ClassId);

                entity.HasOne(e => e.Subject)
                    .WithMany(s => s.ClassExams)
                    .HasForeignKey(e => e.SubjectId);
            });

            modelBuilder.Entity<EFIndividualExam>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.BaseExam)
                    .WithMany(b => b.IndividualExams)
                    .HasForeignKey(e => e.BaseExamId);

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.IndividualExams)
                    .HasForeignKey(e => e.StudentId);
            });

            modelBuilder.Entity<EFExamImage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Exam)
                    .WithMany(ex => ex.Images)
                    .HasForeignKey(e => e.ExamId);
            });

            modelBuilder.Entity<EFClassStudent>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ClassId });

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.ClassStudents)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Class)
                    .WithMany(c => c.ClassStudents)
                    .HasForeignKey(e => e.ClassId);
            });

            modelBuilder.Entity<EFClassTeacher>(entity =>
            {
                entity.HasKey(e => new {e.TeacherId, e.ClassId});

                entity.HasOne(e => e.Class)
                    .WithMany(c => c.ClassTeachers)
                    .HasForeignKey(e => e.ClassId);

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.ClassTeachers)
                    .HasForeignKey(e => e.TeacherId);
            });

            modelBuilder.Entity<EFSubjectStudent>(entity =>
            {
                entity.HasKey(e => new {e.SubjectId, e.StudentId});

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.SubjectStudents)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Subject)
                    .WithMany(s => s.SubjectStudents)
                    .HasForeignKey(e => e.SubjectId);
            });

            modelBuilder.Entity<EFUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasOne(e => e.UserName);

                entity.Property(e => e.Role);
            });

        }
    }
}
