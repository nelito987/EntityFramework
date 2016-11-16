using StudentSystem.Data;
using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem_CodeFirst
{
    class StudentSystem_CodeFirst
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();

            // 1.	Lists all students and their homework submissions. Print only their names and for each homework - content and content-type.
            // ListAllStudentsAndTheirHomeworks(context);

            // 2.List all courses with their corresponding resources. Print the course name and description and everything for each resource. 
            // Order the courses by start date(ascending), then by end date(descending).
            // ListAllCoursesAndResources(context);

            // 3.	List all courses with more than 5 resources. Order them by resources count (descending), 
            // then by start date (descending). Print only the course name and the resource count.
            // CoursesWithMoreThanFiveResources(context);

            // 4.  * List all courses which were active on a given date(choose the date depending on the data seeded to ensure there are results), 
            // and for each course count the number of students enrolled.
            // Print the course name, start and end date, course duration(difference between end and start date) and number of students enrolled.
            // Order the results by the number of students enrolled(in descending order), then by duration(descending).
            //string date = "2016-11-15";
            //AllCoursesForGivenDate(context, DateTime.Parse(date));

            // 5.For each student, calculate the number of courses he / she has enrolled in, 
            // the total price of these courses and the average price per course for the student.
            // Print the student name, number of courses, total price and average price.
            // Order the results by total price(descending), then by number of courses(descending) and then by the student's name (ascending).
            StudentsAndCourses(context);

        }

        // Softuni solution
        //private static void AllStudentsWithCoursesInfo(StudentSystemContext context)
        //{
        //    var students = context.Students
        //        .OrderByDescending(student => student.Courses.Sum(course => course.Price))
        //        .ThenByDescending(student => student.Courses.Count)
        //        .ThenBy(student => student.Name);

        //    foreach (Student student in students)
        //    {
        //        if (student.Courses.Count != 0)
        //        {
        //            Console.WriteLine(
        //                $"{student.Name} - {student.Courses.Count} - {student.Courses.Sum(course => course.Price)} - {student.Courses.Average(course => course.Price)}");
        //        }
        //    }
        //}


        private static void StudentsAndCourses(StudentSystemContext context)
        {
            var students = context.Students
                .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                .ThenByDescending(s => s.Courses.Count)
                .ThenBy(s => s.Name)
                .Select(s => new
                {
                    s.Name,
                    CoursesCount = s.Courses.Count,
                    TotalCoursesPrice = s.Courses.Sum(c => c.Price),
                    AverageCoursesPrice = s.Courses.Average(c => c.Price)
                });

            foreach (var student in students)
            {
                if (student.CoursesCount != 0)
                {
                    Console.WriteLine(
                        $"{student.Name} - {student.CoursesCount} - {student.TotalCoursesPrice} - {student.AverageCoursesPrice}");
                }
            }
        }

        private static void AllCoursesForGivenDate(StudentSystemContext context, DateTime dateTime)
        {
            var courses= context.Courses
                .Where(c => c.StartDate <= dateTime && c.EndDate >= dateTime)
                .OrderByDescending(c => c.Students.Count)
                .OrderByDescending(c => SqlFunctions.DateDiff("day", c.StartDate, c.EndDate))
                .Select(course => new
                {
                    course.Name,
                    course.StartDate,
                    course.EndDate,
                    Duration = SqlFunctions.DateDiff("day", course.StartDate, course.EndDate),
                    StudentsCount = course.Students.Count
                });

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} {course.StartDate} {course.EndDate} {course.Duration} - {course.StudentsCount}");
            }
        }

        private static void CoursesWithMoreThanFiveResources(StudentSystemContext context)
        {
            var courses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources
                });

                foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - {course.Resources.Count}");
            }
        }

        private static void ListAllCoursesAndResources(StudentSystemContext context)
        {
            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources
                });

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} Description: {course.Description}");
                foreach (var r in course.Resources)
                {
                    Console.WriteLine($"--{r.Name} {r.Type} {r.Url}");
                }
            }
        }

        private static void ListAllStudentsAndTheirHomeworks(StudentSystemContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    Homeworks = s.Homeworks
                });

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}");
                foreach (var h in student.Homeworks)
                {
                    Console.WriteLine($"Homework content: {h.Content}, Type: {h.ContentType}");
                }
            }
        }
    }
}
