using SchoolManagementDomain.Core.Models.User.Roles;

namespace SchoolManagementInfrastructure.EF.Models;

public class EFUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
}