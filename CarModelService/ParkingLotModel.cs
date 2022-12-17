using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarModelService
{
    public class ParkingLotModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpaceParkingLotId { get; set; }

        public int parkingLotLevel { get; set; }
        public int parkingLotNumber { get; set; }
        public int Zone { get; set; }

        // check if spaceship is null, if the spaceship id is null, we know now that there's an empty spot at the ParkingLot
        public int? SpaceShipID { get; set; }
        public SpaceShipModel? SpaceShip { get; set; }

        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}