using CarAccessService;
using CarModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UltimateSpaceShipPark.ViewModels;

namespace UltimateSpaceShipPark.Pages
{

    public class RegisterUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly ApplicationDbContext context;
        public ApplicationUser appUser { get; set; }

        [BindProperty]
        public RegisterViewModel Model { get; set; }
        public string Tempregdata { get; set; }
        public RegisterUserModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
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