using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veapp.Api.Repositories;
using vetappback.Entities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServicesTypesRepository servicesTypesRepository;

        public ServiceTypeController(IServicesTypesRepository servicesTypesRepository)
        {
            this.servicesTypesRepository = servicesTypesRepository;

        }

        [HttpGet("GetServiceTypes")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes()
        {
            try
            {
                var serviceTypes = await servicesTypesRepository.GetServiceTypesAsync();
                if (serviceTypes.Count > 0)
                {
                    return Ok(serviceTypes);
                }

                return NotFound();

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        [HttpGet("GetServiceType")]
        public async Task<ActionResult<ServiceType>> GetServiceTypeById(int id)
        {
            try
            {
                var serviceType = await servicesTypesRepository.GetServiceTypeByIdAsync(id);
                if (serviceType != null)
                {
                    return Ok(serviceType);
                }

                return NotFound();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpPost("CreateServiceType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
         public async Task<IActionResult> PostServiceType( [FromBody] ServiceType model)
        {
            

                try
                {
                    if (model!=null)
                    {
                    await servicesTypesRepository.AddServiceTypeAsync(model);

                    return Ok( model);
                        
                    }
                   return BadRequest();

                }
                catch (System.Exception)
                {

                    return NoContent();
                }

        
              
        }

        [HttpPut("{id}")]
        [HttpPut("PutServiceType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> PutServiceType(int id, ServiceType model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await servicesTypesRepository.UpdateServiceTypeAsync(model);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await servicesTypesRepository.ServiceExists(id))
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




    }
}
