using System;
using System.Collections.Generic;

namespace Entity_Framework_Core_Exercises.Data.Models
{
    public partial class Project
    {
        public Project()
        {
            EmployeesProjects = new HashSet<EmpoyeeProject>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<EmpoyeeProject> EmployeesProjects { get; set; }
    }
}
