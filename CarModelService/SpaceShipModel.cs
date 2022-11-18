using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarModelService
{
    public class SpaceShipModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpaceShipID { get; set; }
        [Required]
        public string RegisteringsNummer { get; set; }


        public int? ParkingLotNumber { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }

    }
}
