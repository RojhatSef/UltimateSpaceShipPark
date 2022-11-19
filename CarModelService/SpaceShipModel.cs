using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarModelService
{
    public class SpaceShipModel
    {
        // we don't want the spaceshipId to validate, as it can create error when parkingLot spaceId is being created or updated. 
        [Key]
        [ValidateNever]
        public int? SpaceShipID { get; set; }
        [Required]
        public string RegisteringsNummer { get; set; }


        public int? ParkingLotNumber { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }

    }
}
