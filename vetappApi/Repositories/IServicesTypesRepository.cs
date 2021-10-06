using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> origin/main
using System.Threading.Tasks;
using vetappback.Entities;

namespace veapp.Api.Repositories
{
    public interface IServicesTypesRepository
    {
<<<<<<< HEAD
        IQueryable<ServiceType> GetServiceTypesAsync();
=======
        Task<List<ServiceType>> GetServiceTypesAsync();
>>>>>>> origin/main
        Task<ServiceType> GetServiceTypeByIdAsync(int id);
        Task AddServiceTypeAsync(ServiceType model);
        Task UpdateServiceTypeAsync(ServiceType model);
        Task <bool> ServiceExists(int id);
    }
}