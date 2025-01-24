using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            var employeesDto = _mapper.Map<List<EmployeeDto>>(employees);

            return Ok(employeesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var employeeExists = await _employeeService.GetEmployeeByNameAsync(employeeDto.Name);

            if(employeeExists)
            {
                return BadRequest("Employee already exists");
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            await _employeeService.AddEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPost("CreateEmployeeViaSP")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployeeViaSP(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeExists = await _employeeService.GetEmployeeByNameAsync(employeeDto.Name);

            if (employeeExists)
            {
                return BadRequest("Employee already exists");
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            // Call the service method to add an employee using a stored procedure
            var result = await _employeeService.AddEmployeeUsingStoredProcedureAsync(employee);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the employee.");
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDto.Id }, employeeDto);
        }

        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employeeToUpdate = await _employeeService.GetEmployeeByIdAsync(id);
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            // Check if the new name is already taken by another employee
            var employeeExists = await _employeeService.IsEmployeeNameTakenAsync(employeeDto.Name, id);

            if (employeeExists)
            {
                return BadRequest("Employee name already exists");
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            await _employeeService.UpdateEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeToUpdate.Id }, employeeToUpdate);

        }

       
    }
}
