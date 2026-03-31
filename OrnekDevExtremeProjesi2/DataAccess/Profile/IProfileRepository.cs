using OrnekDevExtremeProjesi2.Models.DTOs;
using OrnekDevExtremeProjesi2.Models;



namespace OrnekDevExtremeProjesi2.DataAccess.Profile
{
    public interface IProfileRepository
    {
        ProfileDataDto GetProfileData(int userId);
        Users GetById(int id);
        bool UserNameExists(string userName, int id);
        void Save();
    }

}