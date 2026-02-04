using StudentInfoSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInfoSystem.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}
