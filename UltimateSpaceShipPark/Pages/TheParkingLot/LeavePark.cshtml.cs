using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages.TheParkingLot
{
    public class LeaveParkModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        // the plan at first was to use repositorys for all channels, but while coding, i noticed the projekt was much smaller than intended. 
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
            }// retrive spaceshipmodel id is only needed, we don't need the entire objekt. 
            SpaceShipModels = _context.SpaceShipModels.FirstOrDefault(n => n.SpaceShipID == id);


            // Calls the transcation class, where we get the pricerate. The prices comes from our database parkinglotmodel where we store the object spacehip.
            if (SpaceShipModels.SpaceShipID != null)
            {
                Transaction transaction = new Transaction();
                SpaceShipModels.ExitTimeEarlierTimeWatcher = DateTime.UtcNow;
                _context.SpaceShipModels.Update(SpaceShipModels);
                _context.SaveChanges();
                if(SpaceShipModels.ExitTimeEarlierTimeWatcher < SpaceShipModels.ExitTime)
                {

                    SpaceShipModels.TotalCost = transaction.PriceRate(SpaceShipModels.EnterTime, SpaceShipModels.ExitTimeEarlierTimeWatcher);
                    SpaceShipModels.CurrentPrice = transaction.PriceRate(SpaceShipModels.ExitTimeEarlierTimeWatcher, SpaceShipModels.ExitTime);
                    TimeSpan CurrentTime = SpaceShipModels.ExitTimeEarlierTimeWatcher - SpaceShipModels.EnterTime;
                    TimeSpan returnMoney = SpaceShipModels.ExitTime - SpaceShipModels.ExitTimeEarlierTimeWatcher; 
                    string outputCurrent = null;
                    int todaldaysCurrent = Convert.ToInt32(CurrentTime.TotalDays);
                    outputCurrent = string.Format("Days {0} Hours {1} Minutes {2} ", todaldaysCurrent, CurrentTime.Hours, CurrentTime.Minutes);
                    FormResult = "Receipt: The total cost for staying with us is: " + Convert.ToString(SpaceShipModels.TotalCost) + "kr. \n SpaceShip:" + SpaceShipModels.RegisteringsNummer + " Stayed with us for: " + outputCurrent + " \n you left at: " + Convert.ToString(SpaceShipModels.ExitTimeEarlierTimeWatcher + " \n Returning: " + SpaceShipModels.CurrentPrice + "Amount back" );
                    return Page();
                }
                else
                {
                    SpaceShipModels.TotalCost = transaction.PriceRate(SpaceShipModels.EnterTime, SpaceShipModels.ExitTime);
                    TimeSpan time = SpaceShipModels.ExitTime - SpaceShipModels.EnterTime;
                    string output = null;
                    int todaldays = Convert.ToInt32(time.TotalDays);
                    output = string.Format("Days {0} Hours {1} Minutes {2} ", todaldays, time.Hours, time.Minutes);
                    FormResult = "Receipt: The total cost for staying with us is: " + Convert.ToString(SpaceShipModels.TotalCost) + "kr. \n SpaceShip:" + SpaceShipModels.RegisteringsNummer + " Stayed with us for: " + output + " \n you left at: " + Convert.ToString(SpaceShipModels.ExitTime);
                    return Page();
                }
         
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
