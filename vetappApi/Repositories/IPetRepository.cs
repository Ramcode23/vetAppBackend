using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IPetRepository
    {
        IQueryable<Pet> GetPetsAsync();
        Task<Pet> GetPetByIdAsync(int id);

        Task AddPetAsync(Pet model);

        Task UpdatePetAsync(Pet model);

        Task<bool> PetExists(int id);

    }
}