using System;
using System.Collections.Generic;

namespace Entity_Framework_Core_Exercises.Data.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public int ManagerId { get; set; }

        public virtual Employee Manager { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
