namespace StudentSystem.Data
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentSystemContext : DbContext
    {        
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany<Course>(student => student.Courses)
                .WithMany(course => course.Students)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("StudentId");
                    configuration.MapRightKey("CourseId");
                    configuration.ToTable("StudentCourses");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}