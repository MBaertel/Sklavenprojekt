using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels;

public class SubjectInput
{
    public string Name { get; set; }

    public EFBaseSubject ToEf()
    {
        return new EFBaseSubject()
        {
            Id = Guid.NewGuid(),
            Name = Name
        };
    }
}