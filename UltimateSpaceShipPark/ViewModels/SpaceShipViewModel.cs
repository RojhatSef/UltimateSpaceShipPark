namespace UltimateSpaceShipPark.ViewModels
{
    public class SpaceShipViewModel
    { // I usually use ViewModels but it never came in use, as i found different path. 
        public int SpaceShipID { get; set; }

        public string? RegisteringsNummer { get; set; }
        public int? ParkingLotNumber { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
    }
}
