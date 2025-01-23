using ProductsAPI.Data;

namespace WebAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> GetEmployeeByName(string employeeName);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
    }
}
