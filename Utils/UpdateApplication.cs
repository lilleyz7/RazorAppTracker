using AppTrackV2.DTO;
using AppTrackV2.Models;

namespace AppTrackV2.Utils
{
    public class UpdateApplication
    {
        public static Application ExecuteApplicationUpdate(ApplicationDto updatedInfo, Application currentInfo)
        {

            currentInfo.Status = updatedInfo.Status;
            currentInfo.Title = updatedInfo.Title;
            currentInfo.Notes = updatedInfo.Notes;
            currentInfo.Company = updatedInfo.Company;
            return currentInfo;
        }
    }
}
