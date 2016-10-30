using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroductionToEntityFramework_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var context = new SoftuniContext();
            // -- 3 and 4
            //var employees = context.Employees
            //    .Where(e => e.Salary > 50000)
            //    .Select(e => new
            //{
            //    e.FirstName,
            //    e.LastName,
            //    e.MiddleName,
            //    e.JobTitle,
            //    e.Salary
            //});

            // -- 3
            //foreach (var e in employees)
            //{
            //    Console.WriteLine("{0} {1} {2} {3} {4}", e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary);
            //}

            // -- 4
            //foreach (var e in employees)
            //{
            //    Console.WriteLine("{0}", e.FirstName);
            //}

            // -- 5
            //var employees = context.Employees
            //    .Where(e => e.Department.Name == "Research and Development")
            //    .OrderBy(e => e.Salary)
            //    .ThenByDescending(e => e.FirstName)
            //    .Select(e => new
            //    {
            //        e.FirstName,
            //        e.LastName,
            //        e.Department,
            //        e.Salary
            //    });

            //foreach (var e in employees)
            //{
            //    Console.WriteLine($"{e.FirstName} {e.LastName} "
            //        + $"from {e.Department.Name} - ${e.Salary:F2}");
            //}

            //-- 6
            //var adress = new Address()
            //{
            //    AddressText = "Vitoshka 15",
            //    TownID = 4
            //};

            //Employee employee = null;
            //var employeeNakov = context.Employees
            //    .FirstOrDefault(e => e.LastName == "Nakov");
            //employeeNakov.Address = adress;
            //context.SaveChanges();

            //var employeesTopTen = context.Employees
            //    .OrderByDescending(e => e.AddressID)
            //    .Take(10)
            //    .Select(e => e.Address.AddressText).ToList();

            //Console.WriteLine(string.Join("\n", employeesTopTen));

            // --- 7
            //var project = context.Projects.Find(2);

            //foreach (var emp in project.Employees)
            //{
            //    var currentEmployee = context.Employees.FirstOrDefault(e => e.EmployeeID == emp.EmployeeID);
            //    currentEmployee.Projects.Remove(project);
            //}

            //context.Projects.Remove(project);
            //context.SaveChanges();

            //var projects = context.Projects
            //    .Take(10)
            //    .Select(p => p.Name)
            //    .ToList();

            //Console.WriteLine(string.Join("\n", projects));

            // --- 8
            var employeesProjects = context.Projects
                .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
                .SelectMany(p => p.Employees)
                .Distinct()
                .Take(30);

            foreach (var emp in employeesProjects)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Manager.FirstName}");
                foreach (var proj in emp.Projects)
                {
                    Console.WriteLine($"--{proj.Name} {proj.StartDate} {proj.EndDate}");
                }
            }
        }
    }
}
