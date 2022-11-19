using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages.TheParkingLot
{
    public class LeaveParkModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IParkingLotRepository parkingLotRepository;
        private readonly ISpaceShipRepository spaceShipRepository;
        public LeaveParkModel(IParkingLotRepository parkingLotRepository, ISpaceShipRepository spaceShipRepository, ApplicationDbContext context)
        {
            this.parkingLotRepository = parkingLotRepository;
            this.spaceShipRepository = spaceShipRepository;
            this._context = context;

        }
        // we send back data to indexEntre for our recite 
        [TempData]
        public string FormResult { get; set; }
        // Varible to store the full payment of how much user needs to pay
        public double Payment { get; set; }

        [BindProperty]
        public ParkingLotModel ParkingLot { get; set; }
        public ParkingLotModel ParkingLotModels { get; set; }
        [BindProperty]
        public SpaceShipModel SpaceShipModels { get; set; }
        // we need to get our spaceship id and pass it along to variables that need it. 
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SpaceShipModels = _context.SpaceShipModels.FirstOrDefault(n => n.SpaceShipID == id);


            // Calls the transcation class, where we get the pricerate. The prices comes from our database parkinglotmodel where we store the object spacehip.
            if (SpaceShipModels.SpaceShipID != null)
            {
                Transaction transaction = new Transaction();
                Payment = transaction.PriceRate(SpaceShipModels.EnterTime, SpaceShipModels.ExitTime);
                TimeSpan time = SpaceShipModels.ExitTime - SpaceShipModels.EnterTime;
                string output = null;
                int todaldays = Convert.ToInt32(time.TotalDays);
                output = string.Format("Days {0} Hours {1} Minutes {2} ", todaldays, time.Hours, time.Minutes);
                FormResult = "Receipt: The total cost for staying with us is: " + Convert.ToString(Payment) + "kr.  You Stayed with us for: " + output + " \n you left at: " + Convert.ToString(SpaceShipModels.ExitTime);
                return Page();
            }
            return new RedirectToPageResult("/TheParkingLot/IndexEntre");


        }
        public IActionResult OnPost()
        {
            //We create a spaceship object, store the id inside, we later use the id we found in the database and delete the spaceship from the
            // database, but before we do that. we need to update the parkinglot ship id to null. If we dont do this, we get foreign key error. 
            SpaceShipModel spaceShip = _context.SpaceShipModels.Find(SpaceShipModels.SpaceShipID);
            var ParkLot2 = _context.ParkingLotModels.FirstOrDefault(n => n.SpaceShipID == SpaceShipModels.SpaceShipID);
            if (spaceShip != null)
            { //check for cascade delete on spaceship set null forigen key
                ParkLot2.SpaceShipID = null;
                _context.ParkingLotModels.Update(ParkLot2);
                _context.SpaceShipModels.Remove(spaceShip);
                _context.SaveChanges();
            }

            return new RedirectToPageResult("/TheParkingLot/IndexEntre");
        }
    }
}
