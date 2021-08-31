
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly DataContext dataContext;

        public PetTypeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("PetTypes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PetType>>> GetPetTypes()
        {
            
            var petTypes = await dataContext.PetTypes.ToListAsync();
            if (petTypes.Count > 0)
            {
                return petTypes;
            }

            return new List<PetType> { };
        }

        [HttpGet("{id}")]
        [HttpGet("PetType")]
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PetType>> GetPetTypeById(int id)
        {
          var petType = await dataContext.PetTypes.FindAsync(id);
            if (petType!=null)
            {
                return petType;
            }

            return null;
        }

        [HttpPost("CreatePetType")]
          [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy ="isAdmin")]
        public async Task<ActionResult<PetType>> PostPetType( [FromBody] PetType model)
        {
          
            try
            {
                
            await dataContext.PetTypes.AddAsync(model);

            return model ;
            }
            catch (System.Exception)
            {
                
           return NoContent();
            }
        }

        [HttpPut("{id}")]
        [HttpPut("EditPetType")]
        public async Task<IActionResult> PutPetType(int id, PetType model)
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
            return dataContext.PetTypes.Any(e => e.Id == id);
        }
     
    }

}