using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTrackV2.Data;
using AppTrackV2.Models;

namespace AppTrackV2.Pages.Applications
{
    public class DetailsModel : PageModel
    {
        private readonly AppTrackV2.Data.ApplicationDbContext _context;

        public DetailsModel(AppTrackV2.Data.ApplicationDbContext context)
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
