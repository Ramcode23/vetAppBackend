using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IPetTypeRepository
    {
         IQueryable<PetType> GetPetTypesAsync();
        Task<PetType> GetPetTypeByIdAsync(int id);
        Task AddPetTypeAsync(PetType model);
        Task UpdatePetTypeAsync(PetType model);
        Task<bool> PetTypeExists(int id);
    }
}