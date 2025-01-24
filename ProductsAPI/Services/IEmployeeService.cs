using ProductsAPI.Data;

namespace WebAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<bool> GetEmployeeByNameAsync(string employeeName);
        Task AddEmployeeAsync(Employee employee);

        Task<bool> AddEmployeeUsingStoredProcedureAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);

        Task<bool> IsEmployeeNameTakenAsync(string employeeName, int currentEmployeeId);
    }
}
