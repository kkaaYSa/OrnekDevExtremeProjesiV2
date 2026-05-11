using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OrnekDevExtremeProjesi2.DataAccess.Logging
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly AppDbContext _db;

        public ActivityLogRepository()
        {
            _db = new AppDbContext();
        }

        public void AddLog(ActivityLog log)
        {
            _db.ActivityLogs.Add(log);
            _db.SaveChanges();
        }

        public List<ActivityLogListDto> GetLogsByMainId(int mainId)
        {
            return _db.ActivityLogs
                .Include(x => x.RequestingUser)
                .Where(x => x.MainId == mainId)
                .OrderByDescending(x => x.LogDate)
                .Select(x => new ActivityLogListDto
                {
                    Id = x.Id,
                    MainId = x.MainId,
                    Action = x.Action,
                    Description = x.Description,
                    LogDate = x.LogDate,
                    UsersId = x.UsersId,
                    UserName = x.RequestingUser != null ? x.RequestingUser.UserName : "Bilinmeyen"
                })
                .ToList();
        }

        public List<ActivityLogListDto> GetAllLogs()
        {
            return _db.ActivityLogs
                .Include(x => x.RequestingUser)
                .OrderByDescending(x => x.LogDate)
                .Select(x => new ActivityLogListDto
                {
                    Id = x.Id,
                    MainId = x.MainId,
                    Action = x.Action,
                    Description = x.Description,
                    LogDate = x.LogDate,
                    UsersId = x.UsersId,
                    UserName = x.RequestingUser != null ? x.RequestingUser.UserName : "Bilinmeyen"
                })
                .ToList();
        }
    }
}
