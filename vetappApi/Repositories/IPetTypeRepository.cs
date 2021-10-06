using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> origin/main
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IPetTypeRepository
    {
<<<<<<< HEAD
         IQueryable<PetType> GetPetTypesAsync();
=======
        Task<List<PetType>> GetPetTypesAsync();
>>>>>>> origin/main
        Task<PetType> GetPetTypeByIdAsync(int id);
        Task AddPetTypeAsync(PetType model);
        Task UpdatePetTypeAsync(PetType model);
        Task<bool> PetTypeExists(int id);
    }
}