using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models.DTOs;
using OrnekDevExtremeProjesi2.Models;


namespace OrnekDevExtremeProjesi2.Business.Mains
{
    public interface IMainService
    {
        List<MainListDto> GetMainList(int currentUserId, string currentRole);
        void CreateMain(Main main, int currentUserId);
        void UpdateMain(OrnekDevExtremeProjesi2.Models.Main main, int currentUserId);
        OrnekDevExtremeProjesi2.Models.Main GetById(int id);
        bool DeleteMain(int id, int currentUserId, string currentUserName, string role);
    }
}