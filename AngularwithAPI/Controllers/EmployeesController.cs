using AngularwithAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularwithAPI.Models;

namespace AngularwithAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeDBContext _employeeDbContext;

        public EmployeesController(EmployeeDBContext employeeDBContext)
        {
            _employeeDbContext = employeeDBContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _employeeDbContext.Employees.AddRangeAsync(employeeRequest);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
    {
        var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if(employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _employeeDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _employeeDbContext.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok();
        }




    }
}
