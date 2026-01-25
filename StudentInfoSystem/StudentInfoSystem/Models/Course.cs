using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StudentInfoSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CourseCode { get; set; }
        
        [Required]
        public string CourseName { get; set; }
        
        [Range(1, 8)]
        public int Credits { get; set; }
        
        public string LecturerId { get; set; }
        public ApplicationUser Lecturer { get; set; }
        
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
