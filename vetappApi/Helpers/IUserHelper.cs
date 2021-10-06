using System.Security.Claims;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappback.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetAuthenticaedUserAsync(ClaimsPrincipal User);
        string GetAuthenticaedUserName(ClaimsPrincipal User);
 
    }
}