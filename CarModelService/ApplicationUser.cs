using Microsoft.AspNetCore.Identity;

namespace CarModelService
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<SpaceShipModel>? SpaceShip { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }
        public List<string>? HelpIdes { get; set; }


    }
}
