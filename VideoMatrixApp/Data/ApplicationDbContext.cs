using Microsoft.EntityFrameworkCore;
using VideoMatrixApp.Models;

namespace VideoMatrixApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        //Constructor 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Por si tengo que configurar la bd manualmente en caso de que hiciera falta
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //Referencia a cada tabla de la bd. Igual que en el tutorial pero con mas entidades
        public DbSet<Device> Devices { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileConnection> ProfileConnections { get; set; }

        
    }
}
