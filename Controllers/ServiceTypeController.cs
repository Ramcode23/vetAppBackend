using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetappback.Entities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        public ServiceTypeController()
        {
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes()
        {
            // TODO: Your code here
            await Task.Yield();

            return new List<ServiceType> { };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceType>> GetServiceTypeById(int id)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }

        [HttpPost("")]
        public async Task<ActionResult<ServiceType>> PostServiceType(ServiceType model)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(int id, ServiceType model)
        {
            // TODO: Your code here
            await Task.Yield();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceType>> DeleteServiceTypeById(int id)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }
    }
}