using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;
using System.Collections.Generic;

namespace OrnekDevExtremeProjesi2.DataAccess.Approvals
{
    public interface IApprovalRepository
    {
        List<PendingApprovalDto> GetPendingApprovals();
        ApprovalProcess GetApprovalWithMain(int approvalId);
        Main GetMainById(int mainId);
        void Save();
        void DeleteMain(Main main);
    }
}