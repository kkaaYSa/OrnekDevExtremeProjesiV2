using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.Business.Approvals
{
    public interface IApprovalService
    {
        List<PendingApprovalDto> GetPendingApprovals();
        ApprovalActionResultDto ActionRequest(int approvalId, bool isApprove, int actionUserId, string actionUserName);
    }
}