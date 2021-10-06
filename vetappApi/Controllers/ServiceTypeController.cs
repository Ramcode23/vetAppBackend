using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veapp.Api.Repositories;
using vetappApi.DTOs;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Utilities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
      private readonly IServicesTypesRepository  repository;
        private readonly IMapper mapper;

        public ServiceTypeController(
            IServicesTypesRepository repository,
            IMapper mapper
            
            )
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("GetServiceTypes")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes([FromQuery] PaginationsDTO pagination)
        {
            try
            {
                 var queryable = repository.GetServiceTypesAsync();
                 await HttpContext.InsertPagintationToHeader(queryable);
                 var serviceTypes = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync();
            
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
                var serviceType = await repository.GetServiceTypeByIdAsync(id);
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
        public async Task<IActionResult> PostServiceType([FromBody] ServiceTypeCreateDTO ServiceTypeCreateDTO)
        {


            try
            {
                if (ServiceTypeCreateDTO != null)
                {
                    var serviceType = mapper.Map<ServiceType>(ServiceTypeCreateDTO);

                    await repository.AddServiceTypeAsync(serviceType);

                    return Ok(serviceType);

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
        public async Task<IActionResult> PutServiceType( int id,[FromBody] ServiceType model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.UpdateServiceTypeAsync(model);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.ServiceExists(id))
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
