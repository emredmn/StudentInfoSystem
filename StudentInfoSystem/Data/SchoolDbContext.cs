using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Data
{
    public class SchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Course>()
                .HasOne(c => c.Lecturer)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role_admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role_lecturer", Name = "Lecturer", NormalizedName = "LECTURER" },
                new IdentityRole { Id = "role_student", Name = "Student", NormalizedName = "STUDENT" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser
            {
                Id = "user_admin",
                UserName = "admin@school.com",
                NormalizedUserName = "ADMIN@SCHOOL.COM",
                Email = "admin@school.com",
                NormalizedEmail = "ADMIN@SCHOOL.COM",
                EmailConfirmed = true,
                FullName = "Admin User"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "123");

            var lecturer = new ApplicationUser
            {
                Id = "user_lecturer",
                UserName = "lecturer@school.com",
                NormalizedUserName = "LECTURER@SCHOOL.COM",
                Email = "lecturer@school.com",
                NormalizedEmail = "LECTURER@SCHOOL.COM",
                EmailConfirmed = true,
                FullName = "Dr. Teacher"
            };
            lecturer.PasswordHash = hasher.HashPassword(lecturer, "123");

            var student = new ApplicationUser
            {
                Id = "user_student",
                UserName = "student@school.com",
                NormalizedUserName = "STUDENT@SCHOOL.COM",
                Email = "student@school.com",
                NormalizedEmail = "STUDENT@SCHOOL.COM",
                EmailConfirmed = true,
                FullName = "John Student",
                StudentNumber = "2024001"
            };
            student.PasswordHash = hasher.HashPassword(student, "123");

            builder.Entity<ApplicationUser>().HasData(admin, lecturer, student);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "role_admin", UserId = "user_admin" },
                new IdentityUserRole<string> { RoleId = "role_lecturer", UserId = "user_lecturer" },
                new IdentityUserRole<string> { RoleId = "role_student", UserId = "user_student" }
            );

            builder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science", Code = "CS" },
                new Department { Id = 2, Name = "Mathematics", Code = "MATH" }
            );

            builder.Entity<Course>().HasData(
                new Course { Id = 1, CourseCode = "MATH101", CourseName = "Calculus I", Credits = 4, LecturerId = "user_lecturer", DepartmentId = 2 },
                new Course { Id = 2, CourseCode = "CS101", CourseName = "Intro to CS", Credits = 3, LecturerId = "user_lecturer", DepartmentId = 1 }
            );
        }
    }
}
