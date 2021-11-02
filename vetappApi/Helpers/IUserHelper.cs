using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using vetappback.DTOs;
using vetappback.Entities;

namespace vetappback.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetAuthenticaedUserAsync(ClaimsPrincipal User);
        string GetAuthenticaedUserName(ClaimsPrincipal User);
        Task<List<Owner>> GetUsersAsync();
        Task<Owner> GetUserByIdAsync(int id);
        Task<Owner> GetProfileAsync(string userName);
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateAdminAsync(RegisterUser registeruser);
        Task<bool> AdminUserExists();
        Task<IdentityResult> CreateoOwnerAsync(RegisterUser registeruser);
        Task UpdateUserAsync(User user);
        Task<SignInResult> PasswordSignInAsync(UserCredentials credentials);
        Task<AuthenticationResponse> BuildTokenAsync(UserCredentials credentials);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task<string> GeneratePasswordResetTokenAsync(User user);

        bool UserExists(string id);
    }
}