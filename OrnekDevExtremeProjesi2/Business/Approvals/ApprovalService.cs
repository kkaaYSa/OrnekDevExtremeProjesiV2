using OrnekDevExtremeProjesi2.Business.Logging;
using OrnekDevExtremeProjesi2.DataAccess.Approvals;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System;
using System.Collections.Generic;


namespace OrnekDevExtremeProjesi2.Business.Approvals
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _approvalRepository;
        private readonly IActivityLogService _activityLogService;

        public ApprovalService()
        {
            _approvalRepository = new ApprovalRepository();
            _activityLogService = new ActivityLogService();
        }

        public List<PendingApprovalDto> GetPendingApprovals()
        {
            return _approvalRepository.GetPendingApprovals();
        }
        public ApprovalActionResultDto ActionRequest(int approvalId, bool isApprove, int actionUserId, string actionUserName)
        {
            var request = _approvalRepository.GetApprovalWithMain(approvalId);
            string islemTuru = "";
            string mesaj = "";

            if (request == null)
            {
                return new ApprovalActionResultDto
                {
                    Success = false,
                    Message = "Onay kaydı bulunamadı."
                };
            }
            if (isApprove)
            {
                var main = _approvalRepository.GetMainById(request.MainId);

                if (main == null)
                {
                    return new ApprovalActionResultDto
                    {
                        Success = false,
                        Message = "Silinecek kayıt bulunamadı."
                    };
                }

                _approvalRepository.DeleteMain(main);
                request.Status = "Onaylandı";
                islemTuru = "Silme Onayı";
                mesaj = $"Admin ({actionUserName}) silme talebini onayladı. Kayıt uçuruldu.";
            }
            else
            {
                request.Status = "Reddedildi";
                islemTuru = "Silme Reddi";
                mesaj = $"Admin ({actionUserName}) silme talebini reddetti.";

            }
            _activityLogService.AddLog(request.MainId, islemTuru, mesaj, actionUserId);
            _approvalRepository.Save();
            return new ApprovalActionResultDto
            {
                Success = true,
                Message = isApprove ? "Talep onaylandı." : "Talep reddedildi."
            };


        }

    }
}