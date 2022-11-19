using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages.TheParkingLot
{

    public class IndexEntreModel : PageModel
    {
        private readonly ISpaceShipRepository _spaceShipRepository;

        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ParkingLotModel ParkSpot { get; set; }

        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public int formresult2 { get; set; }


        public IEnumerable<ParkingLotModel> ParkingSpot { get; set; }
        public IEnumerable<SpaceShipModel> spaceShipModels { get; set; }
        // search term was a function i wanted to implement, but it would probably be a future projekt. 
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }
        public IndexEntreModel(ISpaceShipRepository spaceShipRepository, ApplicationDbContext context = null)
        {
            this._spaceShipRepository = spaceShipRepository;
            this._context = context;
        }

        public void OnGet()
        {
            //getting our models for looping later. 
            ParkingSpot = _context.ParkingLotModels;
            spaceShipModels = _spaceShipRepository.search(searchTerm);
        }

    }
}
