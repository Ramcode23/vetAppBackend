using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;
using vetappback.DTOs;
using vetappback.Utilities;
using vetappApi.Repositories;
using AutoMapper;

//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {

        private readonly IPetRepository repository;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string container = "Pets";
        public PetController(IPetRepository repository,
         IMapper mapper,
         IFileStorage fileStorage)
        {

            this.repository = repository;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet("GetPet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPets([FromQuery] PaginationsDTO pagination)
        {
            var queryable = repository.GetPetsAsync();
            await HttpContext.InsertPagintationToHeader(queryable);
            var pets = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync();

            if (pets.Count > 0)
            {

                return mapper.Map<List<PetDTO>>(pets);
            }


            return new List<PetDTO> { };
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PetDTO>> GetPetById(int id)
        {
            var pet = await repository.GetPetByIdAsync(id);

            if (pet == null)
            {
                return mapper.Map<PetDTO>(pet);
            }


            return null;
        }

        [HttpPost("CreatePet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> PostPet([FromForm] PetCreateDTO petCreateDTO)
        {

            var pet = mapper.Map<Pet>(petCreateDTO);

            if (petCreateDTO.ImageUrl != null)
            {
                pet.ImageUrl = await fileStorage.SaveFile(container, petCreateDTO.ImageUrl);
            }

            await repository.AddPetAsync(pet);

            return NoContent();

        }

        [HttpPut("{id}")]
        [HttpPut("PutPet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> PutPet(int id,[FromBody] PetCreateDTO petCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var pet = await repository.GetPetByIdAsync(id);

            if (pet == null)
            {
                return BadRequest();
            }

            try
            {

                pet = mapper.Map(petCreateDTO, pet);

                if (petCreateDTO.ImageUrl != null)
                {
                    pet.ImageUrl = await fileStorage.SaveFile(container, petCreateDTO.ImageUrl);
                }
                await repository.UpdatePetAsync(pet);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await repository.PetExists(id))
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