using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppTrackV2.Data;
using AppTrackV2.Models;
using Microsoft.AspNetCore.Authorization;
using AppTrackV2.Services;
using AppTrackV2.DTO;
using Microsoft.AspNetCore.Identity;

namespace AppTrackV2.Pages.Applications
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class CreateModel : PageModel
    {
        private readonly IApplicationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IApplicationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationDto ApplicationToAdd { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("./Login");
            }
            var app = await _service.AddApplicationAsync(ApplicationToAdd, userId);

            if (app.Error != null)
            {
                ModelState.AddModelError(string.Empty, app.Error);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
