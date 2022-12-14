using CarModelService;
using Microsoft.EntityFrameworkCore;

namespace CarAccessService
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ParkingLotModel> ParkingLotModels { get; set; }
        public DbSet<SpaceShipModel> SpaceShipModels { get; set; }
        //i let Entitiy framework do the binding of relationship. This can be a hassle later, but in small projekts i find it very good
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }

    }
}
