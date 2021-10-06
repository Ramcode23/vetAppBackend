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
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository repository;
        private readonly IMapper mapper;

        public HistoryController(
            IHistoryRepository repository,
            IMapper mapper
        )
        {
            this.mapper = mapper;
            this.repository = repository;


        }

        [HttpGet("GetHistories")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
<<<<<<< HEAD
        public async Task<ActionResult<IEnumerable<HistoryDTO>>> GetHistorys(int Id,[FromQuery] PaginationsDTO pagination)
        {
            var queryable = repository.GetHistoriesAsync(Id);
=======
        public async Task<ActionResult<IEnumerable<HistoryDTO>>> GetHistorys([FromQuery] PaginationsDTO pagination)
        {
            var queryable = repository.GetHistoriesAsync();
>>>>>>> origin/main
            await HttpContext.InsertPagintationToHeader(queryable);
            var histories = await queryable.OrderBy(x => x.Date).Paginate(pagination).ToListAsync();

            if (histories.Count > 0)
            {

                return mapper.Map<List<HistoryDTO>>(histories);
            }


            return new List<HistoryDTO> { };
        }

        [HttpGet("{id}")]
        [HttpGet("GetHistory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<HistoryDTO>> GetHistoryById([FromQuery] int id)
        {
           var history = await repository.GetHistoryByIdAsync(id);

            if (history == null)
            {
                return mapper.Map<HistoryDTO>(history);
            }


            return null;
        }

        [HttpPost("CreateHistory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<History>> PostHistory([FromBody] HistoryCreateDTO historyCreateDTO)
        {


            var history = mapper.Map<History>(historyCreateDTO);

            
            await repository.AddHistoryAsync(history);

            return NoContent();
        }

        [HttpPut("{id}")]
        [HttpPost("EditteHistory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> PutHistory(int id, HistoryCreateDTO historyCreateDTO)
        {
              var history = await repository.GetHistoryByIdAsync(id);

            if (history == null)
            {
                return BadRequest();
            }

            try
            {

                history = mapper.Map(historyCreateDTO, history);

                await repository.UpdateHistoryAsync(history);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await repository.HistoryExists(id))
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