using Microsoft.AspNetCore.Identity;

namespace CarModelService
{
    public class ApplicationUser : IdentityUser
    {
        public List<SpaceShipModel> SpaceShip { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

    }
}
