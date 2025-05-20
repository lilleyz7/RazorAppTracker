using AppTrackV2.Data;
using AppTrackV2.DTO;
using AppTrackV2.Models;
using AppTrackV2.Utils;
using AppTrackV2.Utils.Types;
using Microsoft.EntityFrameworkCore;

namespace AppTrackV2.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        public ApplicationService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<ServerResponse<Application?>> AddApplicationAsync(ApplicationDto application, string userId)
        {
            var user = await _context.Users.Include(a => a.applications).FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServerResponse<Application?>(null, "invalid user");
            }

            Application newApplication = ApplicationMapper.Map(application, user);

            await _context.Applications.AddAsync(newApplication);
            int changesMade = await _context.SaveChangesAsync();
            if (changesMade == 0)
            {
                return new ServerResponse<Application?>(null, "Unable to add application");
            }

            return new ServerResponse<Application?>(newApplication, null);
        }

        public async Task<ServerResponse<Guid?>> DeleteApplicationAsync(Guid id, string userId)
        {
            var user = await _context.Users.Include(u => u.applications).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServerResponse<Guid?>(null, "Invalid user");
            }

            var application = user.applications.Where(i => i.Id == id).FirstOrDefault();
            if (application == null)
            {
                return new ServerResponse<Guid?>(id, "Application does not exist");
            }

            _context.Applications.Remove(application);

            int changesMade = await _context.SaveChangesAsync();

            if (changesMade == 0)
            {
                return new ServerResponse<Guid?>(id, "Invalid user");
            }

            return new ServerResponse<Guid?>(id, null);
        }

        public async Task<ServerResponse<Application?>> GetApplicationByIdAsync(Guid id, string userId)
        {
            var user = await _context.Users.Include(u => u.applications).FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServerResponse<Application?>(null, "invalid user");
            }

            var application = user.applications.Where(a => a.Id == id).FirstOrDefault();
            if (application == null)
            {
                return new ServerResponse<Application?>(null, "Application does not exist");
            }

            return new ServerResponse<Application?>(application, null);
        }

        public async Task<ServerResponse<IEnumerable<Application>?>> GetApplicationsByUserAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.applications).FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServerResponse<IEnumerable<Application>?>(null, "invalid user");
            }

            return new ServerResponse<IEnumerable<Application>?>(user.applications, null);
        }

        public async Task<ServerResponse<Application?>> UpdateApplicationAsync(ApplicationDto applicationUpdatedInfo, string userId, Guid appId)
        {
            var user = await _context.Users.Include(u => u.applications).FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ServerResponse<Application?>(null, "invalid user");
            }

            var existingApplication = user.applications.FirstOrDefault(a => a.Id == appId);

            if (existingApplication is null)
            {
                return new ServerResponse<Application?>(null, "Invalid application id");
            }

            existingApplication = UpdateApplication.ExecuteApplicationUpdate(applicationUpdatedInfo, existingApplication);
            int changesMade = await _context.SaveChangesAsync();

            if (changesMade == 0)
            {
                return new ServerResponse<Application?>(null, "Unable to update information");
            }

            return new ServerResponse<Application?>(existingApplication, null);

        }
    }
}
