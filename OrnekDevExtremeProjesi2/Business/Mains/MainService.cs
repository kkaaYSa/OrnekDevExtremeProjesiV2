using System;
using System.Collections.Generic;
using System.Linq;
using OrnekDevExtremeProjesi2.DataAccess.Mains;
using OrnekDevExtremeProjesi2.Models.DTOs;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Business.Logging;

namespace OrnekDevExtremeProjesi2.Business.Mains
{
    public class MainService : IMainService
    {
        private readonly IMainRepository _mainRepository;
        private readonly IActivityLogService _activityLogService;

        public MainService()
        {
            _mainRepository = new MainRepository();
            _activityLogService = new ActivityLogService();
        }

        public List<MainListDto> GetMainList(int currentUserId, string currentRole)
        {
            var data = _mainRepository.GetMainListRawData();

            var threeDaysAgo = DateTime.Now.AddDays(-3);

            foreach (var item in data)
            {
                if (currentRole == "Admin")
                {
                    item.DeleteStatus = "Yok";
                    continue;
                }

                if (item.LastApprovalUserId == currentUserId)
                {
                    if (item.LastApprovalStatus == "Reddedildi" && item.LastApprovalDate < threeDaysAgo)
                    {
                        item.DeleteStatus = "Yok";
                    }
                    else
                    {
                        item.DeleteStatus = item.LastApprovalStatus;
                    }
                }
                else
                {
                    item.DeleteStatus = "Yok";
                }
            }

            return data;
        }
        public void CreateMain(Main main, int currentUserId)
        {
            if (string.IsNullOrEmpty(main.Title))
                return;

            main.CreatedDate = DateTime.Now;
            main.IsActive = true;

            _mainRepository.Add(main);
            _mainRepository.Save();

            _activityLogService.AddLog(
                main.Id,
                "Ekleme",
                main.Title + " başlıklı kayıt oluşturuldu.",
                currentUserId
            );
        }
        public void UpdateMain(OrnekDevExtremeProjesi2.Models.Main main, int currentUserId)
        {
            var dbKayit = _mainRepository.GetById(main.Id);
            if (dbKayit == null)
                return;

            dbKayit.Title = main.Title;
            dbKayit.Description = main.Description;
            dbKayit.CategoryId = main.CategoryId;
            dbKayit.IsActive = main.IsActive;

            _mainRepository.Save();

            _activityLogService.AddLog(
                main.Id,
                "Güncelleme",
                "Kayıt detayları güncellendi.",
                currentUserId
            );
        }
        public OrnekDevExtremeProjesi2.Models.Main GetById(int id)
        {
            return _mainRepository.GetById(id);
        }
        public bool DeleteMain(int id, int currentUserId, string currentUserName, string role)
        {
            var record = _mainRepository.GetById(id);
            if (record == null)
                return false;

            if (role != "Admin")
            {
                var request = new OrnekDevExtremeProjesi2.Models.ApprovalProcess
                {
                    MainId = id,
                    UsersId = currentUserId,
                    RequestDate = System.DateTime.Now,
                    Status = "Bekliyor"
                };

                _mainRepository.AddApproval(request);
                _mainRepository.Save();

                _activityLogService.AddLog(
                    id,
                    "Silme Talebi",
                    $"{currentUserName} tarafından silme talebi oluşturuldu.",
                    currentUserId
                );

                return true;
            }

            _mainRepository.Delete(record);
            _mainRepository.Save();

            _activityLogService.AddLog(
                id,
                "Silme",
                $"Admin ({currentUserName}) tarafından silme işlemi gerçekleşti.",
                currentUserId
            );

            return true;
        }
    }
}