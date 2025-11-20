using SchoolManagementDomain.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClassStudent
    {
        public EFClass Class { get; set; }
        public Guid ClassId { get; set; }

        public EFStudent Student { get; set; }
        public Guid StudentId { get; set; }
    }
}
