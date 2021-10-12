using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using AutoMapper;
=======
>>>>>>> origin/main
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veapp.Api.Repositories;
<<<<<<< HEAD
using vetappApi.DTOs;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Utilities;
=======
using vetappback.Entities;
>>>>>>> origin/main
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
<<<<<<< HEAD
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
            
=======
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
>>>>>>> origin/main
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
<<<<<<< HEAD
                var serviceType = await repository.GetServiceTypeByIdAsync(id);
=======
                var serviceType = await servicesTypesRepository.GetServiceTypeByIdAsync(id);
>>>>>>> origin/main
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
<<<<<<< HEAD
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



=======
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

        
              
>>>>>>> origin/main
        }

        [HttpPut("{id}")]
        [HttpPut("PutServiceType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
<<<<<<< HEAD
        public async Task<IActionResult> PutServiceType( int id,[FromBody] ServiceType model)
=======
        public async Task<IActionResult> PutServiceType(int id, ServiceType model)
>>>>>>> origin/main
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
<<<<<<< HEAD
                await repository.UpdateServiceTypeAsync(model);
=======
                await servicesTypesRepository.UpdateServiceTypeAsync(model);
>>>>>>> origin/main
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
<<<<<<< HEAD
                if (!await repository.ServiceExists(id))
=======
                if (!await servicesTypesRepository.ServiceExists(id))
>>>>>>> origin/main
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

<<<<<<< HEAD
<<<<<<< HEAD
            return NoContent();
=======
          return NoContent();
>>>>>>> origin/main
=======
     
>>>>>>> 46ce231... add test history
        }




    }
}
