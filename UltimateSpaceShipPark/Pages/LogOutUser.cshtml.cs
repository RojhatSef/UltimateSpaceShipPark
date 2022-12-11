using CarModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages.Log
{
    [Authorize]
    public class LogoutUserModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public LogoutUserModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLogOutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostDontLogOutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}
