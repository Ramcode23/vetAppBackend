using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DataContext dataContext;
        public PetRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Pet> GetPetsAsync()
        {
            return dataContext.Pets
            .Include(pt => pt.PetType)
            .Include(a => a.Owner)
           .ThenInclude(o => o.User).AsQueryable();
        }


        public Task<Pet> GetPetByIdAsync(int id)
        {
            return dataContext.Pets
            .Include(pt => pt.PetType)
             .Include(a => a.Owner)
            .ThenInclude(o => o.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task AddPetAsync(Pet model)
        {
            dataContext.Pets.AddAsync(model);
            return dataContext.SaveChangesAsync();
        }
        public Task UpdatePetAsync(Pet model)
        {
            dataContext.Entry(model).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();
        }

        public Task<bool> PetExists(int id)
        {
            return dataContext.Pets.AnyAsync(e => e.Id == id);
        }



    }
}