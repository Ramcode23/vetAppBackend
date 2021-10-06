using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> origin/main
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace veapp.Api.Repositories
{
    public class ServicesTypesRepository : IServicesTypesRepository
    {
        private readonly DataContext dataContext;
        public ServicesTypesRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


<<<<<<< HEAD
     
            public IQueryable<ServiceType> GetServiceTypesAsync()
        {
             return dataContext.ServiceTypes.AsQueryable();
          
        }



=======
        public Task<List<ServiceType>> GetServiceTypesAsync()
        {
            return dataContext.ServiceTypes.ToListAsync();
        }


>>>>>>> origin/main
        public Task<ServiceType> GetServiceTypeByIdAsync(int id)
        {
            return dataContext.ServiceTypes
             .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public Task AddServiceTypeAsync(ServiceType model)
        {
            dataContext.ServiceTypes.AddAsync(model);
            return dataContext.SaveChangesAsync();
        }
        public Task UpdateServiceTypeAsync(ServiceType model)
        {
            dataContext.Entry(model).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();
        }

        public Task<bool> ServiceExists(int id)
        {
            return dataContext.ServiceTypes.AnyAsync(e => e.Id == id);
        }

     

    }
}

