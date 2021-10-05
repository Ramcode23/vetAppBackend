using System.Collections.Generic;
using System.Threading.Tasks;
using vetappback.Entities;

namespace veapp.Api.Repositories
{
    public interface IServicesTypesRepository
    {
        Task<List<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType> GetServiceTypeByIdAsync(int id);
        Task AddServiceTypeAsync(ServiceType model);
        Task UpdateServiceTypeAsync(ServiceType model);
        Task <bool> ServiceExists(int id);
    }
}