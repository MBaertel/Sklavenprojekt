using SchoolManagementDomain.Core.Models.User.Roles;

namespace SchoolManagementDomain.Core.Models.User;

public class User
{
    Guid UserId { get; set; }
    string UserName { get; set; }
    Role Role { get; set; }
}