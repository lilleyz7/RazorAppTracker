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
using AppTrackV2.DTO;

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

        public IList<Application> Applications { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "dateAdded";

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "asc";

        public int AppliedCount { get; set; } =  0;
        public int RejectedCount { get; set; } = 0;
        public int InterviewingCount { get; set; } = 0;


        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToPage("./Login");
            }

            var applicationsResult = await _service.GetApplicationsByUserAsync(userId);
            if (applicationsResult.Error != null)
            {
                ModelState.AddModelError(string.Empty, applicationsResult.Error);
                return Page();
            }

            if(applicationsResult.Data == null)
            {
                Applications = new List<Application>();
                return Page();
            }

            var rawApplications = applicationsResult.Data;

            switch (SortField)
            {
                case "title":
                    rawApplications = SortOrder == "desc"
                        ? rawApplications.OrderByDescending(p => p.Title)
                        : rawApplications.OrderBy(p => p.Title);
                    break;

                case "status":
                    rawApplications = SortOrder == "desc"
                        ? rawApplications.OrderByDescending(p => p.Status)
                        : rawApplications.OrderBy(p => p.Status);
                    break;
                case "dateAdded":
                    rawApplications = SortOrder == "desc"
                        ? rawApplications.OrderByDescending(p => p.DateAdded)
                        : rawApplications.OrderBy(p => p.DateAdded);
                    break;

            }

            AppliedCount = rawApplications.Where(a => a.Status == "applied").Count();
            InterviewingCount = rawApplications.Where(a => a.Status == "interviewed").Count();
            RejectedCount = rawApplications.Where(a => a.Status == "rejected").Count();

            Applications = rawApplications.ToList();
            return Page();
        }
    }
}
