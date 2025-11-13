using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchülerManagementDomain.Core
{
    public class Exam
    {
        public int Score { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime HeldAt { get; set; }
    }
}
