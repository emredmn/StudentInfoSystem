using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<List<Course>> GetLecturerCoursesAsync(string lecturerId);
        Task<List<Enrollment>> GetCourseStudentsAsync(int courseId);
        Task<bool> AssignGradeAsync(int enrollmentId, string grade);
        Task CreateCourseAsync(Course course);
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<(ApplicationUser User, string Role)>> GetAllUsersWithRolesAsync();
        Task CreateUserAsync(ApplicationUser user, string password, string role);
        Task DeleteUserAsync(string userId);
        Task DeleteCourseAsync(int courseId);
    }
}
