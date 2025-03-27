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

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeer()
        {
            return await _appDbContext.Employees.ToListAsync();
        }
    }
}
