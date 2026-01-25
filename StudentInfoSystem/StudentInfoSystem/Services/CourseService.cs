using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseService(SchoolDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AssignGradeAsync(int enrollmentId, string grade)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
            if (enrollment == null) return false;

            enrollment.Grade = grade;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserAsync(ApplicationUser user, string password, string role)
        {
            if (role == "Student" && string.IsNullOrEmpty(user.StudentNumber))
            {
                var random = new Random();
                user.StudentNumber = DateTime.Now.Year.ToString() + random.Next(1000, 9999).ToString();
            }

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Lecturer)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<List<(ApplicationUser User, string Role)>> GetAllUsersWithRolesAsync()
        {
            var query = from user in _context.Users
                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                        join role in _context.Roles on userRole.RoleId equals role.Id
                        select new { User = user, Role = role.Name };

            var result = await query.ToListAsync();
            return result.Select(x => (x.User, x.Role)).ToList();
        }

        public async Task<List<Enrollment>> GetCourseStudentsAsync(int courseId)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<List<Course>> GetLecturerCoursesAsync(string lecturerId)
        {
            return await _context.Courses
                .Where(c => c.LecturerId == lecturerId)
                .Include(c => c.Enrollments)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
