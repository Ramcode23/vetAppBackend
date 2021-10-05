
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetappApi.Repositories;
using vetappback.Entities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeRepository repository;

        public PetTypeController(IPetTypeRepository repository)
        {
            this.repository = repository;

        }

        [HttpGet("getPetTypes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PetType>>> GetPetTypes()
        {

            var petTypes = await repository.GetPetTypesAsync();
            if (petTypes.Count > 0)
            {
                return petTypes;
            }

            return new List<PetType> { };
        }

        [HttpGet("{id}")]
        [HttpGet("getPetType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PetType>> GetPetTypeById(int id)
        {
            var petType = await repository.GetPetTypeByIdAsync(id);
            if (petType != null)
            {
                return petType;
            }

            return NotFound();
        }

        [HttpPost("CreatePetType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<ActionResult<PetType>> PostPetType([FromBody] PetType model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                await repository.AddPetTypeAsync(model);

                return Ok(model);
            }
            catch (System.Exception)
            {

                return NoContent();
            }
        }

        [HttpPut("{id}")]
        [HttpPut("EditPetType")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<IActionResult> PutPetType(int id, PetType model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.UpdatePetTypeAsync(model);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.PetTypeExists(id))
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