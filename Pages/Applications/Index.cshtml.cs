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
    public class IndexModel : PageModel
    {
        private readonly IApplicationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IApplicationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public IList<Application> Application { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToPage("");
            }

            var applicationsResult = await _service.GetApplicationsByUserAsync(userId);
            if (applicationsResult.Error != null)
            {
                ModelState.AddModelError(string.Empty, applicationsResult.Error);
                return Page();
            }

            if(applicationsResult.Data == null)
            {
                Application = new List<Application>();
                return Page();
            }

            Application = applicationsResult.Data.ToList();
            return Page();
        }
    }
}
