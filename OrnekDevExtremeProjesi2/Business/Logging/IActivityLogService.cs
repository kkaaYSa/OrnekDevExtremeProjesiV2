using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrnekDevExtremeProjesi2.Business.Logging
{
    public interface IActivityLogService
    {
        void AddLog(int mainId, string action, string description, int userId);
        List<ActivityLogListDto> GetLogsByMainId(int mainId);
    }
}