using OrnekDevExtremeProjesi2.Models.DTOs;


namespace OrnekDevExtremeProjesi2.Business.Profile
{
    public interface IProfileService
    {
        ProfileDataDto GetProfileData(int userId);
        UpdateProfileResultDto UpdateProfile(int id, string userName, string oldPassword, string newPassword, int currentUserId);
    }
}