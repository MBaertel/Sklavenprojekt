using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels;

public class IndividualExamInput
{
    public EFClassExam BaseExam { get; set; }
    
    public Guid BaseExamId { get; set; }

    public int Score { get; set; }

    public EFStudent Student { get; set; }
    
    public Guid StudentId { get; set; }

    public int Grade { get; set; }
    
    public ICollection<EFExamImage> Images { get; set; }

    public EFIndividualExam ToEf(EFStudent student, EFClassExam baseExam)
    {
        return new EFIndividualExam
        {
            Id = Guid.NewGuid(),
            BaseExam =  baseExam,
            BaseExamId = baseExam.Id,
            Score = Score,
            Student =  student,
            StudentId = student.Id,
            Grade = Grade,
            Images = Images
        };
    }
}