﻿using Entity_Framework_Core_Exercises.Data.Models;

namespace Entity_Framework_Core_Exercises
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            
            //03. Employee Full Information Test
            Console.WriteLine(GetEmployeesFullInformation(context));
            
            //04. Employees with Salary Over 50 000 Test
            Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            
            //05. Employees from Research and Development Test
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
        }
        
        //03. Employee Full Information
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
        //04. Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e=>new 
                {   e.FirstName, 
                    e.Salary
                })
                .Where(e=>e.Salary>50000)
                .OrderBy(e=>e.FirstName)
                .ToList();
            return string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} - {e.Salary:f2}"));
        }
        //05. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e =>
                    new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Department.Name,
                        e.Salary
                    })
                .OrderBy(e => e.Salary)
                .ThenBy(e => e.FirstName);
                
            return string.Join(Environment.NewLine,employees.Select(x=>$"{x.FirstName} {x.LastName} from {x.Name} - {x.Salary:c2}"));
        }
    }
}