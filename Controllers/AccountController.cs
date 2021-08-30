using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager,
         IConfiguration configuration,
         SignInManager<User> signInManager,
         DataContext dataContext,
         IUserHelper userHelper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        [HttpGet("GetUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<IEnumerable<Owner>>> GetUsers()
        {
            var owners = await dataContext.Owners.Include(u => u.User).ToListAsync();
            if (owners.Count > 1)
            {
                return owners;
            }
            return new List<Owner>();

        }

        [HttpGet("{id}")]
        [HttpGet("GetUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<Owner>> GetUserById(int id)
        {
            var owner = await dataContext.Owners.Include(u => u.User).FirstOrDefaultAsync(o => o.Id == id);
            if (owner != null)
            {
                return owner;
            }
            return BadRequest("Owner does not exist");

        }

        [HttpGet("GetProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Owner>> GetProfile()
        {
            var owner = await dataContext.Owners
            .Include(u => u.User)
            .FirstOrDefaultAsync(o => 
            o.User.Email == userHelper.GetAuthenticaedUserName(User));
            if (owner != null)
            {
                return owner;
            }
            return BadRequest("Owner does not exist");

        }


        [HttpPost("createmanager")]
        public async Task<ActionResult<AuthenticationResponse>> createAdmin([FromBody] RegisterUser registeruser)
        {

             var isExits = await userManager.FindByEmailAsync(registeruser.Email);
            if (isExits == null)
            {

                var user = new User
                {
                    UserName = registeruser.Email,
                    Email = registeruser.Email,
                    Document = registeruser.Document,
                    FirstName = registeruser.FirstName,
                    LastName = registeruser.LastName,
                    Address = registeruser.Address,
                };

                var credencials = new UserCredentials { Password = registeruser.Password, Email = registeruser.Email };
                var rest = await userManager.CreateAsync(user, registeruser.Password);
                await userManager.AddClaimAsync(user, new Claim("role", "manager"));
                var owner = new Manager { User = user };
                await dataContext.AddAsync(owner);
                await dataContext.SaveChangesAsync();
                if (rest.Succeeded)
                {
                    return await BuildToken(credencials);
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }
        [HttpPost("createOwner")]
          [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> PostOwner([FromBody] RegisterUser registeruser)
        {
            var isExits = await userManager.FindByEmailAsync(registeruser.Email);
            if (isExits == null)
            {

                var user = new User
                {
                    UserName = registeruser.Email,
                    Email = registeruser.Email,
                    Document = registeruser.Document,
                    FirstName = registeruser.FirstName,
                    LastName = registeruser.LastName,
                    Address = registeruser.Address,
                };

                var credencials = new UserCredentials { Password = registeruser.Password, Email = registeruser.Email };
                var rest = await userManager.CreateAsync(user, registeruser.Password);
                await userManager.AddClaimAsync(user, new Claim("role", "owner"));
                var owner = new Owner { User = user };
                await dataContext.AddAsync(owner);
                await dataContext.SaveChangesAsync();
                if (rest.Succeeded)
                {
                    return await BuildToken(credencials);
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
        public async Task<IActionResult> PutUser(string id, User model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(User).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
            var resp = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (resp.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return BadRequest("Login error");
            }
        }


        private async Task<AuthenticationResponse> BuildToken(UserCredentials credentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",credentials.Email)
            };

            var user = await userManager.FindByEmailAsync(credentials.Email);
            var claimBD = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimBD);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }

        private bool UserExists(string id)
        {
            return dataContext.Users.Any(e => e.Id == id);
        }


    }


}