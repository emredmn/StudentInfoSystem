using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StudentInfoSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
