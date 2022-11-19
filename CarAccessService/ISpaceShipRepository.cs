using CarModelService;

namespace CarAccessService
{
    public interface ISpaceShipRepository
    {  // i scaled this projekt to become much larger than it became. I belived i needed repostiories for all, but i later found
        // better solutions for these small ones
        IEnumerable<SpaceShipModel> search(string searchTerm);
        IEnumerable<SpaceShipModel> GetAllShips();

        SpaceShipModel GetSpaceShip(int id);

        SpaceShipModel Update(SpaceShipModel updatedShip);

        SpaceShipModel Add(SpaceShipModel newdatedShip);

        SpaceShipModel Delete(int id);
    }
}
