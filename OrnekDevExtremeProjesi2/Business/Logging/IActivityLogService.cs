using OrnekDevExtremeProjesi2.Models.DTOs;
using System.Collections.Generic;

namespace OrnekDevExtremeProjesi2.Business.Logging
{
    public interface IActivityLogService
    {
        void AddLog(int mainId, string action, string description, int userId);
        List<ActivityLogListDto> GetLogsByMainId(int mainId);
        List<ActivityLogListDto> GetAllLogs();
    }
}