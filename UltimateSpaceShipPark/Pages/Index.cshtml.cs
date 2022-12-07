﻿using CarAccessService;
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

        public async Task<IActionResult> OnGet(string id)
        {
            var groupUser = await userManager.GetUserAsync(User);

            if (TempRegData != null)
            {



                var tempShip = context.SpaceShipModels.FirstOrDefault(o => o.RegisteringsNummer == TempRegData);

                groupUser.SpaceShip = new List<SpaceShipModel> { tempShip };
                context.Update(groupUser);
                context.SaveChanges();

            }



            return Page();

        }
    }
}