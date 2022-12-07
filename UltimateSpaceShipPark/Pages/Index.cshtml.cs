using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly UserManager<ApplicationUser> userManager;
        public SpaceShipModel spaceShipModel { get; set; }


        public ApplicationUser appUser { get; set; }
        private readonly ApplicationDbContext context;
        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            this.userManager = userManager;
            this.context = context;

        }
        [TempData]
        public string TempRegData { get; set; }

        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public int formresult2 { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            var groupUser = await userManager.GetUserAsync(User);

            if (TempRegData != null && groupUser != null)
            {



                var tempShip = context.SpaceShipModels.FirstOrDefault(o => o.RegisteringsNummer == TempRegData);
                var OldUser = groupUser.SpaceShip.Select(reg => reg.RegisteringsNummer == TempRegData);

                if (tempShip.RegisteringsNummer == TempRegData)
                {

                    List<SpaceShipModel> SpaceShip = groupUser.SpaceShip.ToList();
                    List<SpaceShipModel> newList = new List<SpaceShipModel>();
                    foreach (var item in SpaceShip)
                    {
                        newList.Add(item);
                    }
                    groupUser.SpaceShip = newList;
                }
                else if (tempShip.RegisteringsNummer == null)
                {
                    return new RedirectToPageResult("/TheParkingLot/IndexEntre");
                }
                else
                {
                    groupUser.SpaceShip = new List<SpaceShipModel> { tempShip };

                }


                context.Update(groupUser);
                context.SaveChanges();

            }



            return Page();

        }
    }
}