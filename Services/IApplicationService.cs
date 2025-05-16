using AppTrackV2.Models;
using AppTrackV2.DTO;

namespace AppTrackV2.Services
{
    public interface IApplicationService
    {
        IEnumerable<Application> GetApplicationsByUserAsync(string userId);
        IEnumerable<Application> GetApplicationByTitleAsync(string title, string userId);
        Task<Application> GetApplicationByIdAsync(Guid id, string userId);

        Task AddApplicationAsync(ApplicationDto application, string userId);
        Task UpdateApplicationAsync(ApplicationDto application, string userId, Guid appId);
        Task DeleteApplicationAsync(Guid id, string userId);
    }
}
