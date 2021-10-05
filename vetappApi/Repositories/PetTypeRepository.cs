using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public class PetTypeRepository
    {
        private readonly DataContext dataContext;
        public PetTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public Task<List<PetType>> GetPetTypesAsync()
        {
            return dataContext.PetTypes.ToListAsync();
        }


        public Task<PetType> GetPetTypeByIdAsync(int id)
        {
            return dataContext.PetTypes
             .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public Task AddPetTypeAsync(PetType model)
        {
            dataContext.PetTypes.AddAsync(model);
            return dataContext.SaveChangesAsync();
        }
        public Task UpdatePetTypeAsync(PetType model)
        {
            dataContext.Entry(model).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();
        }

        public Task<bool> PetTypeExists(int id)
        {
            return dataContext.PetTypes.AnyAsync(e => e.Id == id);
        }

     

    }

}