namespace UltimateSpaceShipPark.ViewModels
{
    public class ParkingViewModel
    {// I usually use ViewModels but it never came in use, as i found different path. 
        public int SpaceParkingLotId { get; set; }

        public int parkingLotLevel { get; set; }
        public int parkingLotNumber { get; set; }

        // check if spaceship is null, if the spaceship id is null, we know now that there's an empty spot at the ParkingLot
        public int SpaceShipID { get; set; }
        public string SpaceShip { get; set; }
    }
}
