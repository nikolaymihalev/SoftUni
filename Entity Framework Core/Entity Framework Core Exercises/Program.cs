using Entity_Framework_Core_Exercises.Data.Models;

namespace Entity_Framework_Core_Exercises
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            
            //03. Employee Full Information
            Console.WriteLine(GetEmployeesFullInformation(context));
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                           .Select(e=>new 
                               {   e.FirstName, 
                                   e.LastName,
                                   e.MiddleName,
                                   e.JobTitle,
                                   e.Salary
                               }).ToList();
            return string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));
        }
    }
}