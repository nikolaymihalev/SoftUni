using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework_Core_Exercises.Data.Models
{
    public class EmpoyeeProject
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
