using CRM.Server.Data;
using CRM.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //DI
        private readonly AppDbContext _appDbContext;

        public EmployeesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_appDbContext.Employees
        }

        //GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            _appDbContext.Employees.Add(emp);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeById", new { id = emp.Id }, emp);
        }


        //https://localhost:7291/api/Employees/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var emp = await _appDbContext.Employees.FindAsync(id);
            //var emp = await _appDbContext.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
            //return Ok(emp);
        }

        //https://localhost:7291/api/Employees/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployeeById(int id, Employee emp)
        {
            if (id != emp.Id)
            {
                BadRequest();
            }

            _appDbContext.Entry(emp).State = EntityState.Modified;
            try
            {
                await _appDbContext.SaveChangesAsync(); //çalıştırılacak kod bloğu
            }
            catch (Exception ex)
            {
                if (!EmployeeExists(id)) 
                {
                    return NotFound(ex);
                }
                else
                {
                    throw; //boş geç
                }
            }
            return NoContent(); 
        }
        private bool EmployeeExists(int id)
        {
            return _appDbContext.Employees.Any(e => e.Id == id);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployeeById(int id)
        {
            var emp = await _appDbContext.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            _appDbContext.Employees.Remove(emp);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
