using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using vetappback.DTOs;
using vetappback.Entities;

namespace vetappback.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext dataContext;
 private readonly IConfiguration configuration;
        public UserHelper(
         DataContext dataContext,
         UserManager<User> userManager,
         RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager,
           IConfiguration configuration)
        {
            this.dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
             this.configuration = configuration;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<User> GetAuthenticaedUserAsync(ClaimsPrincipal User)
        {
            var authenticatedUser = User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;

            return await _userManager.FindByEmailAsync(authenticatedUser);

        }
        public string GetAuthenticaedUserName(ClaimsPrincipal User)
        {

            return User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;

        }


        public Task<List<Owner>> GetUsersAsync()
        {

            return dataContext.Owners.Include(u => u.User).ToListAsync();

        }
        public Task<Owner> GetUserByIdAsync(int id)
        {

            return dataContext.Owners.Include(u => u.User).FirstOrDefaultAsync(o => o.Id == id);

        }
        public Task<Owner> GetProfileAsync(string userName)
        {

            return dataContext.Owners
            .Include(u => u.User)
            .FirstOrDefaultAsync(o =>
            o.User.Email == userName);

        }

        public async Task<IdentityResult> CreateAdminAsync(RegisterUser registeruser)
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

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            var manager = new Manager { User = user };
            await dataContext.AddAsync(manager);
            await dataContext.SaveChangesAsync();
            await _userManager.AddClaimAsync(user, new Claim("role", "admin"));

            return rest;


        }

        public async Task<IdentityResult> CreateoOwnerAsync(RegisterUser registeruser)
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

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            var owner = new Owner { User = user };
            await dataContext.AddAsync(owner);
            await dataContext.SaveChangesAsync();
            await _userManager.AddClaimAsync(user, new Claim("role", "admin"));

            return rest;

        }
        public Task UpdateUserAsync(User user)
        {

            dataContext.Entry(user).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();

        }


        public async Task<SignInResult> PasswordSignInAsync(UserCredentials credentials)
        {
            return await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        }
        
        public async Task<AuthenticationResponse> BuildTokenAsync(UserCredentials credentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",credentials.Email)
            };

            var user = await _userManager.FindByEmailAsync(credentials.Email);
            var claimBD = await _userManager.GetClaimsAsync(user);
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

        public bool UserExists(string id)
        {
            return dataContext.Users.Any(e => e.Id == id);
        }
        public  Task<bool> AdminUserExists()
        {
            return dataContext.Managers.AnyAsync();
        }
    }
}