using ProductsAPI.Data;

namespace WebAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<Employee> GetEmployeeByNameAsync(string employeeName);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
    }
}
