using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System.Collections.Generic;



namespace OrnekDevExtremeProjesi2.DataAccess.Logging
{
    public interface IActivityLogRepository
    {
        void AddLog(ActivityLog log);
    
    List<ActivityLogListDto> GetLogsByMainId(int mainId);
    }
}