using System.Collections.Generic;
using System.Linq;
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


     
            public IQueryable<ServiceType> GetServiceTypesAsync()
        {
             return dataContext.ServiceTypes.AsQueryable();
          
        }



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
        public Task<bool> AnyServiceExists()
        {
            return dataContext.ServiceTypes.AnyAsync();
        }

     

    }
}

