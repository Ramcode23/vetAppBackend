using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace veapp.Api.Repositories
{
    public interface IServicesTypesRepository
    {
        IQueryable<ServiceType> GetServiceTypesAsync();
        Task<ServiceType> GetServiceTypeByIdAsync(int id);
        Task AddServiceTypeAsync(ServiceType model);
        Task UpdateServiceTypeAsync(ServiceType model);
        Task <bool> ServiceExists(int id);
    }
}