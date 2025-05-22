using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTrackV2.Data;
using AppTrackV2.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppTrackV2.Pages.Applications
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FirstOrDefaultAsync(m => m.Id == id);

            if (application is not null)
            {
                Application = application;

                return Page();
            }

            return NotFound();
        }
    }
}
