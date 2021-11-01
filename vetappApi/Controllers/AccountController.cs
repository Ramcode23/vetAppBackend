using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using vetappback.DTOs;
using vetappback.Entities;
using vetappback.Helpers;

namespace vetappback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserHelper userHelper;
        private readonly IMapper mapper;

        public AccountController(IUserHelper userHelper,
         IMapper mapper
        )
        {

            this.userHelper = userHelper;
            this.mapper = mapper;
        }

        [HttpGet("GetUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetUsers()
        {
            var owners = await userHelper.GetUsersAsync();
            if (owners.Count > 1)
            {
                return mapper.Map<List<OwnerResponse>>(owners);
            }
            return new List<OwnerResponse>();

        }

        [HttpGet("{id}")]
        [HttpGet("GetUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<OwnerResponse>> GetUserById(int id)
        {
            var owner = await userHelper.GetUserByIdAsync(id);
            if (owner != null)
            {
                return mapper.Map<OwnerResponse>(owner);
            }
            return BadRequest("Owner does not exist");

        }

        [HttpGet("GetProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<OwnerResponse>> GetProfile()
        {
            var username = userHelper.GetAuthenticaedUserName(User);
            var owner = await userHelper.GetProfileAsync(username);
            if (owner != null)
            {
                return mapper.Map<OwnerResponse>(owner);
            }
            return BadRequest("Owner does not exist");

        }


        [HttpPost("createmanager")]

        public async Task<ActionResult<AuthenticationResponse>> createAdmin([FromBody] RegisterUser registeruser)
        {

            var isExits = await userHelper.GetUserByEmailAsync(registeruser.Email);
            if (isExits.Email == null)
            {
                var rest = await userHelper.CreateAdminAsync(registeruser);
                var credencials = new UserCredentials { Password = registeruser.Password, Email = registeruser.Email };
                if (rest.Succeeded)
                {
                    return await userHelper.BuildTokenAsync(credencials);
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }
        [HttpPost("createOwner")]

        public async Task<ActionResult<AuthenticationResponse>> PostOwner([FromBody] RegisterUser registeruser)
        {
            var isExits = await userHelper.GetUserByEmailAsync(registeruser.Email);
            if (isExits == null)
            {

                var credencials = new UserCredentials { Password = registeruser.Password, Email = registeruser.Email };
                var rest = await userHelper.CreateoOwnerAsync(registeruser);
                if (rest.Succeeded)
                {
                    return await userHelper.BuildTokenAsync(credencials);
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpPut("{id}")]
        [HttpPost("EditUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await userHelper.UpdateUserAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userHelper.UserExists(id))
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


        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> loging([FromBody] UserCredentials credentials)
        {
            var resp = await userHelper.PasswordSignInAsync(credentials);

            if (resp.Succeeded)
            {
                return await userHelper.BuildTokenAsync(credentials);
            }
            else
            {
                return BadRequest("Login error");
            }
        }






    }


}