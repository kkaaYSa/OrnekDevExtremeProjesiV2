using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System.Collections.Generic;

namespace OrnekDevExtremeProjesi2.DataAccess.Mains
{
    public interface IMainRepository
    {
        List<MainListDto> GetMainListRawData();
        void Add(Main main);
        void Save();
        OrnekDevExtremeProjesi2.Models.Main GetById(int id);

        void Update(OrnekDevExtremeProjesi2.Models.Main main);
        void Delete(OrnekDevExtremeProjesi2.Models.Main main);
        void AddApproval(OrnekDevExtremeProjesi2.Models.ApprovalProcess approval);
    }

}