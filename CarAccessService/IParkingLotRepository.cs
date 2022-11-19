using CarModelService;
namespace CarAccessService
{
    public interface IParkingLotRepository
    {
        // i scaled this projekt to become much larger than it became. I belived i needed repostiories for all, but i later found
        // better solutions for these small ones
        IEnumerable<ParkingLotModel> GetAllParkingLots();
        ParkingLotModel GetParkingSpot(int id);
        ParkingLotModel Update(ParkingLotModel updateParkingSpot);
        ParkingLotModel Add(ParkingLotModel addParkingSpot);
        ParkingLotModel Delete(int id);
    }
}