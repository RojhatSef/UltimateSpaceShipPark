using CarAccessService;
using CarModelService;

namespace UltimateSpaceShipPark
{
    public class SeedData
    {
        private readonly ApplicationDbContext _ApplicationDbContext;

        public SeedData(ApplicationDbContext applicationDbContext)
        {

            this._ApplicationDbContext = applicationDbContext;
        }

        public void seedData()
        {// we seed our database, by looping the parkinglot class 
            // we also ensure our database has been created line 19
            _ApplicationDbContext.Database.EnsureCreated();
            if (!_ApplicationDbContext.ParkingLotModels.Any())
            {
                var parkModels = new List<ParkingLotModel>();
                for (int j = 1; j <= 3; j++)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        ParkingLotModel parkingLotModel = new ParkingLotModel();
                        parkingLotModel.parkingLotLevel = j;
                        parkingLotModel.parkingLotNumber = i + 1;
                        parkingLotModel.Zone = 888; 
                        parkingLotModel.SpaceShipID = null;
                        parkModels.Add(parkingLotModel);
                    }
                }// instead of saving all the time, we add all the data by the range of it
                _ApplicationDbContext.ParkingLotModels.AddRange(parkModels);
                _ApplicationDbContext.SaveChanges();
            }
        }
    }
}
