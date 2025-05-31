using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTrackV2.Data;
using AppTrackV2.Models;
using AppTrackV2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AppTrackV2.Pages.Applications
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly IApplicationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(IApplicationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("./Login");
            }
            var applicationResult = await _service.GetApplicationByIdAsync(id, userId);

            if (applicationResult.Error is not null)
            {
                Application = applicationResult.Data!;

                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("./Login");
            }

            var applicationResult = await _service.DeleteApplicationAsync(id, userId);

            if (applicationResult.Error is not null)
            {
                ModelState.AddModelError(string.Empty, applicationResult.Error);
                return Page();
            }


            return RedirectToPage("./Index");
        }
    }
}
