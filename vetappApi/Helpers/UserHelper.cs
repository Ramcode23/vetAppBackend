using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using vetappback.Entities;

namespace vetappback.Helpers
{
    public class UserHelper:IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext dataContext;

        public UserHelper(
         DataContext dataContext,
         UserManager<User> userManager,
         RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager)
        {
            this.dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
        public  string GetAuthenticaedUserName(ClaimsPrincipal User)
        {
           
            return  User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;

        }

        
    }
}