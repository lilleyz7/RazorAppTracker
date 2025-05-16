namespace AppTrackV2.Services
{
    public interface IApplicationService
    {
        ICollection<Application> GetApplications(string userId);
        ICollection<Application> GetByTitleAsync(string title, string userId);
        Task<Application> GetByIdAsync(Guid id, string userId);

        Task AddApplication(ApplicationDto application, string userId);
        Task UpdateApplication(ApplicationDto application, string userId, Guid appId);
        Task DeleteApplication(Guid id, string userId);
    }
}
