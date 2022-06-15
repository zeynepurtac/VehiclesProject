using Microsoft.EntityFrameworkCore;
namespace VehicleProje.Models
{
    public class VehicleContext:DbContext
    {
        public VehicleContext(DbContextOptions options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HC4315D;Initial Catalog=Vehicles; Integrated Security=True");

        }
        public DbSet<Color> Colors { get; set; }  
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Car> Cars { get; set; }


    }
}
