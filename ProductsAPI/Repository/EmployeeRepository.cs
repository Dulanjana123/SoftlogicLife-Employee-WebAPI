using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;

namespace WebAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly APIDbContext _context;

        public EmployeeRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(p => p.Id == employeeId);
            return employee;
        }

        public async Task<Employee> GetEmployeeByName(string employeeName)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(p => p.Name == employeeName);
            return employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _context.Employee
                .ToListAsync();
            return employees;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}

