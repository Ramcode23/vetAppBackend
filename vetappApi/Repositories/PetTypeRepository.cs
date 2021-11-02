using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public class PetTypeRepository:IPetTypeRepository
    {
        private readonly DataContext dataContext;
        public PetTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


      
            public IQueryable<PetType> GetPetTypesAsync()
        {
             return dataContext.PetTypes.AsQueryable();
          
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
        public Task<bool> AnyPetTypeExists()
        {
            return dataContext.PetTypes.AnyAsync();
        }

     

    }

}