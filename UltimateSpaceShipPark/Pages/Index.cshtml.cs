using CarModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UltimateSpaceShipPark.ViewModels;

namespace UltimateSpaceShipPark.Pages
{

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public RegisterViewModel Model { get; set; }
        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //this might be wrong, as we are creating a userModel and we're looking for identityUsers. Need to check this later
                var usermail = await userManager.FindByEmailAsync(Model.Email);
                if (usermail == null)
                {

                    //var spaceshipModel = new SpaceShipModel { RegisteringsNummer = Model.Email, EnterTime = null, ExitTime = null, ExitTimeEarlierTimeWatcher = null,  };
                    var user = new ApplicationUser { UserName = Model.Email, Email = Model.Email, SpaceShip = null };
                    var result = await userManager.CreateAsync(user, Model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToPage("/Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Email is already in use");

                }

            }
            return Page();
        }
    }
}