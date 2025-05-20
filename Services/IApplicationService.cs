using AppTrackV2.Models;
using AppTrackV2.DTO;
using AppTrackV2.Utils.Types;

namespace AppTrackV2.Services
{
    public interface IApplicationService
    {
        Task<ServerResponse<IEnumerable<Application>?>> GetApplicationsByUserAsync(string userId);
        Task<ServerResponse<Application?>> GetApplicationByIdAsync(Guid id, string userId);

        Task<ServerResponse<Application?>> AddApplicationAsync(ApplicationDto application, string userId);
        Task<ServerResponse<Application?>> UpdateApplicationAsync(ApplicationDto applicationUpdatedInfo, string userId, Guid appId);
        Task<ServerResponse<Guid?>> DeleteApplicationAsync(Guid id, string userId);
    }
}
