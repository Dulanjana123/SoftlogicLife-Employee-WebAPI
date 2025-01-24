using ProductsAPI.Data;

namespace WebAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<bool> GetEmployeeByNameAsync(string employeeName);
        Task AddEmployee(Employee employee);

        Task<bool> AddEmployeeUsingStoredProcedure(Employee employee);
        Task UpdateEmployee(Employee employee);

        Task<bool> IsEmployeeNameTakenAsync(string employeeName, int currentEmployeeId);
    }
}
