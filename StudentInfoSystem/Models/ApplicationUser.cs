using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StudentInfoSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? StudentNumber { get; set; }
        
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> TaughtCourses { get; set; }
    }
}
