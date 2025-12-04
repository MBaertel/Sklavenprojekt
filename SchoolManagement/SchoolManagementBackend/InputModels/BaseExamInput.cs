using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels;

public class BaseExamInput
{
    public DateTime Date { get; set; }

    public EFClassExam ToEf()
    {
        return new EFClassExam
        {
            Id = Guid.NewGuid(),
            Date = Date,
        };
    }
}