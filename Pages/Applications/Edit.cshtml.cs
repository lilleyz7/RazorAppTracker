using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTrackV2.Data;
using AppTrackV2.Models;
using AppTrackV2.DTO;
using AppTrackV2.Utils;
using AppTrackV2.Services;
using Microsoft.AspNetCore.Identity;
using AppTrackV2.Utils.Types;

namespace AppTrackV2.Pages.Applications
{
    public class EditModel : PageModel
    {
        private readonly IApplicationService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IApplicationService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationDto Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("./Login");
            }

            ServerResponse<Application?> applicationResponse =  await _service.GetApplicationByIdAsync(id, userId);
            if (applicationResponse.Error != null)
            {
                ModelState.AddModelError(string.Empty, applicationResponse.Error);
                return Page();
            }

            if (applicationResponse.Data == null)
            {
                return Page();
            }

            Application = ApplicationMapper.ReverseMap(applicationResponse.Data);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
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

            var applicationResponse = await _service.GetApplicationByIdAsync(id, userId);
            if (applicationResponse.Error != null)
            {
                ModelState.AddModelError(string.Empty, applicationResponse.Error);
                return Page();
            }
            var updateResponse = await _service.UpdateApplicationAsync(Application, userId, applicationResponse.Data!.Id);

            if (updateResponse.Error != null)
            {
                ModelState.AddModelError(string.Empty, updateResponse.Error);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
