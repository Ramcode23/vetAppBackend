using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly DataContext dataContext;
        public ServiceTypeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("GetServiceTypes")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes()
        {
            var serviceTypes = await dataContext.ServiceTypes.ToListAsync();
            if (serviceTypes.Count>0)
            {
                return serviceTypes;
            }

            return new List<ServiceType> { };
        }

        [HttpGet("{id}")]
        [HttpGet("GetServiceType")]
        public async Task<ActionResult<ServiceType>> GetServiceTypeById(int id)
        {
            var serviceType = await dataContext.ServiceTypes.FindAsync(id);
            if (serviceType!=null)
            {
                return serviceType;
            }

            return null;
        }

        [HttpPost("CreateServiceType")]
        public async Task<ActionResult<ServiceType>> PostServiceType(ServiceType model)
        {
             try
            {
                
            await dataContext.ServiceTypes.AddAsync(model);

            return model ;
            }
            catch (System.Exception)
            {
                
           return NoContent();
            }
        }

        [HttpPut("{id}")]
         [HttpPut("PutServiceType")]
        public async Task<IActionResult> PutServiceType(int id, ServiceType model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(model).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

     
         private bool PetTypeExists(int id)
        {
            return dataContext.ServiceTypes.Any(e => e.Id == id);
        }
     
    }
    }
