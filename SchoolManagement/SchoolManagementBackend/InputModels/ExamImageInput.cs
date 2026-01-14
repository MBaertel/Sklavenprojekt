using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels;

public class ExamImageInput
{
    public Guid Id { get; set; }
    public EFIndividualExam Exam { get; set; }
    public Guid ExamId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public byte[] ImageData { get; set; }

    public EFExamImage ToEf()
    {
        return new EFExamImage
        {
            Id = Id,
            Exam = Exam,
            ExamId = ExamId,
            Name = Name,
            Link = Link,
            ImageData = ImageData
        };
    }
}