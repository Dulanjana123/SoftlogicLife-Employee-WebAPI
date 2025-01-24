using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using WebAPI.Models;

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

        public async Task<bool> AddEmployeeUsingStoredProcedure(Employee employee)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("@Epf", employee.Epf),
                new SqlParameter("@Mobile", employee.Mobile),
                new SqlParameter("@Address", employee.Address),
                new SqlParameter("@Email", employee.Email)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC AddEmployee @Name, @Epf, @Mobile, @Address, @Email", parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //SP that need to Create in the ms sql server
        //CREATE PROCEDURE AddEmployee
        //@Name NVARCHAR(100),
        //@Epf INT,
        //@Mobile NVARCHAR(20),
        //@Address NVARCHAR(MAX),
        //@Email NVARCHAR(100)
        //AS
        //BEGIN
        //SET NOCOUNT ON;

        //-- Insert the employee into the database
        //INSERT INTO Employee(Name, Epf, Mobile, Address, Email)
        //VALUES(@Name, @Epf, @Mobile, @Address, @Email);
        //END;



        public async Task<Employee> GetEmployee(int employeeId)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(p => p.Id == employeeId);
            return employee;
        }

        public async Task<bool> GetEmployeeByNameAsync(string employeeName)
        {
            var employee = await _context.Employee
                .AnyAsync(p => p.Name == employeeName);
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

        public async Task<bool> IsEmployeeNameTakenAsync(string employeeName, int currentEmployeeId)
        {
            return await _context.Employee.AnyAsync(e => e.Name == employeeName && e.Id != currentEmployeeId);
        }

    }
}

