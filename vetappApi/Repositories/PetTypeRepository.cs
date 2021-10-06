using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> origin/main
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
<<<<<<< HEAD
    public class PetTypeRepository:IPetTypeRepository
=======
    public class PetTypeRepository
>>>>>>> origin/main
    {
        private readonly DataContext dataContext;
        public PetTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


<<<<<<< HEAD
      
            public IQueryable<PetType> GetPetTypesAsync()
        {
             return dataContext.PetTypes.AsQueryable();
          
        }

=======
        public Task<List<PetType>> GetPetTypesAsync()
        {
            return dataContext.PetTypes.ToListAsync();
        }


>>>>>>> origin/main
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