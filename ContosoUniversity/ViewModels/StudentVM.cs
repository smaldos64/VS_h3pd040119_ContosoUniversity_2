using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Tools;

namespace ContosoUniversity.ViewModels
{
    public class StudentVM
    {
        [MatchParent("ID")]
        public int ID { get; set; }

        [MatchParent("LastName")]
        public string LastName { get; set; }

        [MatchParent("FirstMidName")]
        public string FirstMidName { get; set; }

        [MatchParent("EnrollmentDate")]
        public DateTime EnrollmentDate { get; set; }
    }
}
