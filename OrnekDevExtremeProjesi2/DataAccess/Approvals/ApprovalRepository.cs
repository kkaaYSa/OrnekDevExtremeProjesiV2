using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.DataAccess.Approvals
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly AppDbContext _db;

        public ApprovalRepository()
        {
            _db = new AppDbContext();
        }

        public List<PendingApprovalDto> GetPendingApprovals()
        {
            return _db.ApprovalProcess
                .Include(x => x.RequestingUser)
                .Include(x => x.RelatedMain)
                .Where(x => x.Status == "Bekliyor")
                .Select(x => new PendingApprovalDto
                {
                    Id = x.Id,
                    MainId = x.MainId,
                    TargetTitle = x.RelatedMain != null ? x.RelatedMain.Title : "Kayıt Bulunamadı",
                    TargetDescription = x.RelatedMain != null ? x.RelatedMain.Description : "Kayıt Bulunamadı",
                    RequestedBy = x.RequestingUser != null ? x.RequestingUser.UserName : "Bilinmeyen",
                    RequestDate = x.RequestDate,
                    Status = x.Status
                })
                .ToList();
        }
        public ApprovalProcess GetApprovalWithMain(int approvalId)
        {
            return _db.ApprovalProcess
                     .Include(x => x.RelatedMain)
                     .FirstOrDefault(x => x.Id == approvalId);
        }

        public Main GetMainById(int mainId)
        {
            return _db.Mains.FirstOrDefault(x => x.Id == mainId);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void DeleteMain(Main main)
        {
            _db.Mains.Remove(main);
        }
    }
}