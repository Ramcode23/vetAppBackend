using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappback.Models {
    public class DataContext : IdentityDbContext<User> {
        public DataContext() { }
        public DataContext(DbContextOptions options) : base (options) { }


        public DbSet<Owner> Owners { get; set; }
        public DbSet<PetType> PetTypes { get; set; }

    	public DbSet<Pet> Pets { get; set; }

    	public DbSet<ServiceType> ServiceTypes { get; set; }

    	public DbSet<History> Histories { get; set; }

    	public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Manager> Managers{ get; set; }
       



    }
}