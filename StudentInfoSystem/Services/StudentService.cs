using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<double> CalculateGpaAsync(string studentId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && e.Grade != null)
                .ToListAsync();

            if (!enrollments.Any()) return 0.0;

            double totalPoints = enrollments.Sum(e => GradeToPoints(e.Grade) * e.Course.Credits);
            int totalCredits = enrollments.Sum(e => e.Course.Credits);

            if (totalCredits == 0) return 0.0;

            return Math.Round(totalPoints / totalCredits, 2);
        }

        public async Task<List<Course>> GetAvailableCoursesAsync(string studentId)
        {
            var enrolledCourseIds = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.CourseId)
                .ToListAsync();

            return await _context.Courses
                .Include(c => c.Lecturer)
                .Where(c => !enrolledCourseIds.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetMyGradesAsync(string studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<int> GetTotalCreditsAsync(string studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .SumAsync(e => e.Course.Credits);
        }

        public async Task<bool> RegisterCourseAsync(string studentId, int courseId)
        {
            bool exists = await _context.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (exists) return false;

            var currentCredits = await GetTotalCreditsAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            if (currentCredits + course.Credits > 30)
            {
                return false; 
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        private double GradeToPoints(string grade) => grade switch
        {
            "AA" => 4.0,
            "BA" => 3.5,
            "BB" => 3.0,
            "CB" => 2.5,
            "CC" => 2.0,
            "DC" => 1.5,
            "DD" => 1.0,
            "FF" => 0.0,
            _ => 0.0
        };
    }
}
