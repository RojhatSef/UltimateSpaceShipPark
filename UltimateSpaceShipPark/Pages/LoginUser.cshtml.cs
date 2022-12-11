using CarModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UltimateSpaceShipPark.Pages.Log
{

    public class LoginUserModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        [BindProperty]
        public LoginModel LoginModel { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
        public LoginUserModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;

        }

        public void onGet(string? returnurl)
        {
            ViewData["ReturnUrl"] = returnurl;
            Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, false);
                //if (identityResult.Succeeded)
                //{
                //    if (returnUrl == null || returnUrl == "/")
                //    {
                //        return RedirectToPage("/Index");
                //    }
                //    else
                //    {
                //        return RedirectToPage(returnUrl);
                //    }

                //}
                if (identityResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }

                }

                ModelState.AddModelError("", "username or password incorrect");
            }

            return Page();
        }
    }
}
