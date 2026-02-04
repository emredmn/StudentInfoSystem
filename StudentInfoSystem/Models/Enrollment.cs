using System;

namespace StudentInfoSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        
        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        public string? Grade { get; set; }
        
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
