using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UltimateSpaceShipPark.ViewModels;


namespace UltimateSpaceShipPark.Pages.TheParkingLot
{
    [Authorize]
    public class ParkModel : PageModel
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        [BindProperty]
        public ParkingLotModel ParkLot { get; set; }
        [BindProperty]
        public SpaceShipModel spaceShip { get; set; }
        [DataType(DataType.Date)]

        [BindProperty]
        public DateTime EntryTime { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime ExitTime { get; set; } = DateTime.UtcNow;

        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string TempRegData { get; set; }



        public ParkingViewModel parkinglotviewModel { get; set; }
        public SpaceShipViewModel SpaceShipViewModel { get; set; }

        public ParkModel(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {

            _applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }
        public IActionResult OnGet(int id)
        {

            if (id != null)
            {
                // retrive the parklot object store it in the variable then pass it to the objekt.  I tried using ParkLot object instead of the parklot, but it only retrived the iD and skipped the rest. 
                var parklot2 = _applicationDbContext.ParkingLotModels.FirstOrDefault(n => n.SpaceParkingLotId == id);
                ParkLot = parklot2;

            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {//If userinput is lower than present day, we don't accept it and they need to send a new set of time.  
                if (EntryTime < ExitTime)
                {
                    if (EntryTime == ExitTime)
                    {
                        FormResult = "Exist time can't be set to present time. try again";
                        return new RedirectToPageResult("/TheParkingLot/IndexEntre");
                    }
                    var ParkLot2 = _applicationDbContext.ParkingLotModels.FirstOrDefault(n => n.SpaceParkingLotId == ParkLot.SpaceParkingLotId);
                    // we create a new instance of a ship when we park, 
                    // TO DO i should retrive or make so users car/spaceship don't remove 100% as i could easily store them and reuse them instead of removing. 
                    var oldspacesHip = AddSpaceShipTo();
                    var currentUser = await CombineUser(oldspacesHip);
                    var parkUser = CombineSpacePark(oldspacesHip, currentUser);


                    Transaction transaction = new Transaction();
                    // we store the total cost of our spaceship stay in our variable Payment. 
                    oldspacesHip.CurrentPrice = transaction.PriceRate(EntryTime, ExitTime);

                    //calls the Transcation class, 
                    // we use timespan to see datetime between how long our visitor has stayed. 
                    TimeSpan time = ExitTime - EntryTime;
                    string output = null;
                    int todaldays = Convert.ToInt32(time.TotalDays);
                    output = string.Format("Days {0} Hours {1} Minutes {2} ", todaldays, time.Hours, time.Minutes);
                    FormResult = "Receipt: The total cost for staying with us is: " + Convert.ToString(oldspacesHip.CurrentPrice) + "kr.  \n SpaceShip: " + oldspacesHip.RegisteringsNummer + " you are staying with us for: " + output + " \n Your parking starts at: " + Convert.ToString(EntryTime);
                    return new RedirectToPageResult("/TheParkingLot/IndexEntre");

                }// Time can't be less than present time
                FormResult = "Peepop, We don't have a time machine, You tried to enter present time or past time. Try again ";
                return new RedirectToPageResult("/TheParkingLot/IndexEntre");
            }
            else
            {
                FormResult = null;
                FormResult = "Something went wrong, We couldn't park your SpaceShip. Try again";
                return new RedirectToPageResult("/TheParkingLot/IndexEntre");
            }
        }
        public SpaceShipModel AddSpaceShipTo()
        {
            var ParkLot2 = _applicationDbContext.ParkingLotModels.FirstOrDefault(n => n.SpaceParkingLotId == ParkLot.SpaceParkingLotId);
            SpaceShipModel newSpaceShipOnParkingLot = new SpaceShipModel
            {
                EnterTime = EntryTime,
                ExitTime = ExitTime,
                ExitTimeEarlierTimeWatcher = ExitTime,
                RegisteringsNummer = spaceShip.RegisteringsNummer,
                ParkingLotNumber = ParkLot2.parkingLotNumber,
                parkingLotLevel = ParkLot2.parkingLotLevel,
                parkingSpotId = ParkLot.SpaceParkingLotId,
                ParkinglotModel = ParkLot2

            };
            return newSpaceShipOnParkingLot;
        }
        public async Task<ApplicationUser> CombineUser(SpaceShipModel spaceShipModel)
        {
            var groupUser = await userManager.GetUserAsync(User);


            // gets a spaceship mode, we add this spaceshipmodel to our applicationUser, One user can have many spaceships

            groupUser.SpaceShip = new List<SpaceShipModel> { spaceShipModel };
            _applicationDbContext.Update(groupUser);
            _applicationDbContext.SaveChanges();

            return groupUser;


        }
        public ParkingLotModel CombineSpacePark(SpaceShipModel spaceShipModel, ApplicationUser appuser)
        {
            var ParkLot2 = _applicationDbContext.ParkingLotModels.FirstOrDefault(n => n.SpaceParkingLotId == ParkLot.SpaceParkingLotId);
            // UPDATES parkinglot with a spaceship 
            ParkLot2.SpaceShip = spaceShipModel;
            ParkLot2.ApplicationUser = appuser;
            _applicationDbContext.ParkingLotModels.Update(ParkLot2);
            _applicationDbContext.SaveChanges();

            return ParkLot2;

        }
    }
}
