using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetappApi.Repositories;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Utilities;
//using vetappback.Models;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {

        private readonly IAgendaRepository repository;
        private readonly IMapper mapper;

        public AgendaController(
            IAgendaRepository repository,
             IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet("GetAgendas")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<AgendaDTO>>> GetAgendas([FromQuery] PaginationsDTO pagination)
        {
            var queryable = repository.GetAgendasAsync();
            await HttpContext.InsertPagintationToHeader(queryable);
            var agendas = await queryable.OrderBy(x => x.Date).Paginate(pagination).ToListAsync();

            if (agendas.Count > 0)
            {
                return mapper.Map<List<AgendaDTO>>(agendas);
            }

            return new List<AgendaDTO> { };

        }

        
        [HttpGet("GetAgenda")]
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AgendaDTO>> GetAgendaById(int id)
        {
            var agenda = await repository.GetAgendaByIdAsync(id);

            if (agenda == null)
            {
                return mapper.Map<AgendaDTO>(agenda);
            }

            return null;
        }

        [HttpPost("PostAgenda")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<AgendaDTO>> PostAgenda([FromBody] AgendaCreateDTO agendaCrateDTO)
        {

            var agenda = mapper.Map<Agenda>(agendaCrateDTO);
            await repository.AddAgendaAsync(agenda);
            return NoContent();
        }

        [HttpPut("{id}")]
        [HttpPut("PutAngenda")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> PutAgenda(int id, AgendaCreateDTO agendaCrateDTO)
        {

            var agenda = await repository.GetAgendaByIdAsync(id);

            if (agenda == null)
            {
                return BadRequest();
            }

            try
            {
                agenda = mapper.Map(agendaCrateDTO, agenda);

                await repository.UpdateAgendaAsync(agenda);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await repository.AgendaExists(id))
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