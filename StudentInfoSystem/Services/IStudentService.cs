using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Services
{
    public interface IStudentService
    {
        Task<bool> RegisterCourseAsync(string studentId, int courseId);
        Task<List<Enrollment>> GetMyGradesAsync(string studentId);
        Task<double> CalculateGpaAsync(string studentId);
        Task<int> GetTotalCreditsAsync(string studentId);
        Task<List<Course>> GetAvailableCoursesAsync(string studentId);
    }
}
