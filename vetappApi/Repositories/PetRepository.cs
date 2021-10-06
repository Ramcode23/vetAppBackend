using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
<<<<<<< HEAD
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
=======
    public class PetRepository
    {
          private readonly DataContext dataContext;
        public PetRepository(DataContext dataContext)
        {
              this.dataContext = dataContext;
        }
        
            public IQueryable<Pet> GetPetsAsync()
        {
             return dataContext.Pets.AsQueryable();
          
>>>>>>> origin/main
        }


        public Task<Pet> GetPetByIdAsync(int id)
        {
            return dataContext.Pets
<<<<<<< HEAD
            .Include(pt => pt.PetType)
             .Include(a => a.Owner)
            .ThenInclude(o => o.User)
            .FirstOrDefaultAsync(x => x.Id == id);
=======
            .FirstOrDefaultAsync(x=>x.Id==id);
>>>>>>> origin/main
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

<<<<<<< HEAD

=======
     
>>>>>>> origin/main

    }
}