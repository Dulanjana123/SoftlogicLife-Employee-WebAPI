using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using WebAPI.Repository;

namespace WebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddEmployee(employee);
        }

        public async Task<bool> AddEmployeeUsingStoredProcedureAsync(Employee employee)
        {
            return await _employeeRepository.AddEmployeeUsingStoredProcedure(employee);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployee(employeeId);
        }

        public async Task<bool> GetEmployeeByNameAsync(string employeeName)
        {
            return await _employeeRepository.GetEmployeeByNameAsync(employeeName);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<bool> IsEmployeeNameTakenAsync(string employeeName, int currentEmployeeId)
        {
            return await _employeeRepository.IsEmployeeNameTakenAsync(employeeName, currentEmployeeId);
        }

    }
}
