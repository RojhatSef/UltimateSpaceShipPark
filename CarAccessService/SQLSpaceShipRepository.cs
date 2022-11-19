using CarModelService;

namespace CarAccessService
{
    public class SQLSpaceShipRepository : ISpaceShipRepository
    {
        private readonly ApplicationDbContext context;
        public SQLSpaceShipRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public SpaceShipModel Add(SpaceShipModel newdatedShip)
        {
            context.SpaceShipModels.Add(newdatedShip);
            context.SaveChanges();
            return newdatedShip;
        }

        public SpaceShipModel Delete(int id)
        {
            SpaceShipModel spaceShip = context.SpaceShipModels.Find(id);
            if (spaceShip != null)
            {
                context.SpaceShipModels.Remove(spaceShip);
                context.SaveChanges();
            }
            return spaceShip;
        }

        public IEnumerable<SpaceShipModel> GetAllShips()
        {
            return context.SpaceShipModels;
        }

        public SpaceShipModel GetSpaceShip(int id)
        {
            return context.SpaceShipModels.Find(id);
        }

        public IEnumerable<SpaceShipModel> search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.SpaceShipModels;
            }
            return context.SpaceShipModels.Where(e => e.RegisteringsNummer.Contains(searchTerm));
        }

        public SpaceShipModel Update(SpaceShipModel updatedShip)
        {
            var updateSpaceship = context.SpaceShipModels.Attach(updatedShip);
            updateSpaceship.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedShip;
        }
    }
}
