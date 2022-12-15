using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UltimateSpaceShipPark.Pages.TheParkingLot
{
    [Authorize]
    public class IndexEntreModel : PageModel
    {
        private readonly ISpaceShipRepository _spaceShipRepository;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ParkingLotModel ParkSpot { get; set; }

        [TempData]
        public string FormResult { get; set; }

        [TempData]
        public int formresult2 { get; set; }

        [BindProperty]
        public string userIDString { get; set; }
        [BindProperty]
        public string spaceshipIdprop { get; set; }
        [BindProperty]
        public int? IntSpaceID { get; set; }
        [BindProperty]
        public int? IntspaceUserID { get; set; }
        [BindProperty]
        public ApplicationUser applicationUser { get; set; }

        public IEnumerable<ParkingLotModel> ParkingSpot { get; set; }
        public IEnumerable<SpaceShipModel> spaceShipModels { get; set; }
        // search term was a function i wanted to implement, but it would probably be a future projekt. 
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }
        public IndexEntreModel(ISpaceShipRepository spaceShipRepository, ApplicationDbContext context = null, UserManager<ApplicationUser> userManager = null)
        {
            this._spaceShipRepository = spaceShipRepository;
            this._context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            applicationUser = await userManager.GetUserAsync(HttpContext.User);
            //IntspaceUserID = applicationUser.SpaceShip.Select(e => e.SpaceShipID) == ite);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userIDString = userId;
            AddSpaceShipTo(applicationUser);

            return Page();

        }
        public void AddSpaceShipTo(ApplicationUser user)
        {
            if (user.SpaceShip == null)
            {

            }
            else
            {
                //applicationUser.SpaceShip.


            }

            //getting our models for looping later.
            ParkingSpot = _context.ParkingLotModels;
            spaceShipModels = _spaceShipRepository.search(searchTerm);

        }


    }
}
