using AppTrackV2.DTO;
using AppTrackV2.Models;

namespace AppTrackV2.Utils
{
    public class ApplicationMapper
    {
        public static Application Map(ApplicationDto applicationDto, ApplicationUser user)
        {
            return new Application
            {
                Company = applicationDto.Company,
                Title = applicationDto.Title,
                Notes = applicationDto.Notes,
                Status = applicationDto.Status,
                Link = applicationDto.Link,
                DateAdded = DateTime.UtcNow,
                UserId = user.Id,
                ApplicationUser = user,
            };
        }

        public static ApplicationDto ReverseMap(Application app)
        {
            return new ApplicationDto
            {
                Company = app.Company,
                Title = app.Title,
                Notes = app.Notes,
                Status = app.Status,
                Link = app.Link,
            };
        }
    }
}
