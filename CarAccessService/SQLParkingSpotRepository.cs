using CarModelService;

namespace CarAccessService
{
    public class SQLParkingSpotRepository : IParkingLotRepository
    {
        private readonly ApplicationDbContext context;
        public SQLParkingSpotRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ParkingLotModel Add(ParkingLotModel addParkingSpot)
        {
            context.ParkingLotModels.Add(addParkingSpot);
            context.SaveChanges();
            return addParkingSpot;
        }

        public ParkingLotModel Delete(int id)
        {
            ParkingLotModel parkingLotModel = context.ParkingLotModels.Find(id);
            if (parkingLotModel != null)
            {
                context.ParkingLotModels.Remove(parkingLotModel);
                context.SaveChanges();
            }
            return parkingLotModel;
        }

        public IEnumerable<ParkingLotModel> GetAllParkingLots()
        {
            return context.ParkingLotModels;
        }

        public ParkingLotModel GetParkingSpot(int id)
        {
            return context.ParkingLotModels.Find(id);
        }

        public ParkingLotModel Update(ParkingLotModel updateParkingSpot)
        {
            var parkspot = context.ParkingLotModels.Attach(updateParkingSpot);
            parkspot.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updateParkingSpot;
        }
    }
}
