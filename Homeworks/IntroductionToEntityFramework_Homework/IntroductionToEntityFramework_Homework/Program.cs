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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");

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
            //var employeesProjects = context.Projects
            //    .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
            //    .SelectMany(p => p.Employees)
            //    .Distinct()
            //    .Take(30);

            //foreach (var emp in employeesProjects)
            //{
            //    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Manager.FirstName}");
            //    foreach (var proj in emp.Projects)
            //    {
            //        Console.WriteLine($"--{proj.Name} {proj.StartDate} {proj.EndDate}");
            //    }
            //}

            // --- 9

            //var adresses = context.Addresses                
            //    .OrderByDescending(a => a.Employees.Count())
            //    .ThenBy(a => a.Town.Name)
            //    .Take(10);

            //foreach(var a in adresses)
            //{
            //    Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count()} employees");
            //}

            // --- 10 -- to be checked in Profiler
            //var employee147 = context.Employees
            //    .Where(e => e.EmployeeID == 147)
            //    .Select(e => new
            //    {
            //        e.FirstName,
            //        e.LastName,
            //        e.JobTitle,
            //        e.Projects
            //    });

            //foreach(var e in employee147)
            //{
            //    Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
            //    var projects = e.Projects.OrderBy(p => p.Name);

            //    foreach(var p in projects)
            //    {
            //        Console.WriteLine($"{p.Name}");
            //    }
            //}

            // --- 11
            //var departments = context.Departments
            //    .Where(d => d.Employees.Count > 5)
            //    .OrderBy(d => d.Employees.Count());

            //foreach(var d in departments)
            //{
            //    Console.WriteLine($"{d.Name} {d.Employee.FirstName}");
            //    foreach (var emp in d.Employees)
            //    {
            //        Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
            //    }
            //}

            // --- 15
            //var projects = context.Projects
            //    .OrderByDescending(p => p.StartDate)
            //    .Take(10)
            //    .OrderBy(p => p.Name)
            //    .Select(p => new {
            //        p.Name,
            //        p.Description,
            //        p.StartDate,
            //        p.EndDate
            //    });

            //foreach (var project in projects)
            //{
            //    Console.WriteLine($"{project.Name} {project.Description} {project.StartDate} {project.EndDate}");
            //}

            // --- 16
            //var empToIncreaseSalary = context.Employees
            //    .Where(e => e.Department.Name == "Engineering" ||
            //                e.Department.Name == "Tool Design" ||
            //                e.Department.Name == "Marketing" ||
            //                e.Department.Name == "Information Services");

            //foreach(var e in empToIncreaseSalary)
            //{
            //    e.Salary += e.Salary * (decimal)0.12;
            //    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary})");
            //}
            //context.SaveChanges();

            // --- 18
            //var empStartingWith = context.Employees
            //    .Where(e => e.FirstName.Substring(0, 2) == "Sa");

            //foreach (var employee in empStartingWith)
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary})");
            //}

        }
    }
}
