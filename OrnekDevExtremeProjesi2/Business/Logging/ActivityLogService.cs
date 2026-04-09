using System;
using System.Collections.Generic;
using OrnekDevExtremeProjesi2.DataAccess.Logging;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.Business.Logging
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogService()
        {
            _activityLogRepository = new ActivityLogRepository();
        }

        public void AddLog(int mainId, string action, string description, int userId)
        {
            var log = new ActivityLog
            {
                MainId = mainId,
                Action = action,
                Description = description,
                LogDate = DateTime.Now,
                UsersId = userId
            };
            _activityLogRepository.AddLog(log);
        }

        public List<ActivityLogListDto> GetLogsByMainId(int mainId)
        {
            return _activityLogRepository.GetLogsByMainId(mainId);
        }

        public List<ActivityLogListDto> GetAllLogs()
        {
            return _activityLogRepository.GetAllLogs();
        }
    }
}