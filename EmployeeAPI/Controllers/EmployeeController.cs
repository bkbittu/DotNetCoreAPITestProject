using EmployeeAPI.Model;
using EmployeeAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        private readonly IJWTAuthenticationmanager _JWTAuthenticationmanager;

        public EmployeeController(IEmployeeRepository _employeeRepository, IJWTAuthenticationmanager jWTAuthenticationmanager)
        {
            employeeRepository = _employeeRepository;
            _JWTAuthenticationmanager = jWTAuthenticationmanager;
        }

        
        [HttpGet("GetEmployees")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employeeRepository.GetEmployees();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
          string token=  _JWTAuthenticationmanager.Authenticate(userCred.Username, userCred.password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
