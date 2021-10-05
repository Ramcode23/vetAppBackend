using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace vetappback.Utilities
{
    public interface IFileStorage
    {
        Task<string> SaveFile(string container, IFormFile file);
        Task DeleteFile(string route, string container);
        Task<string> EditFile(string container, IFormFile file, string route);
    }
}