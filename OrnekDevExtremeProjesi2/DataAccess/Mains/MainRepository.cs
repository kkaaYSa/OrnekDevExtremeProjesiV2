using System.Collections.Generic;
using System.Linq;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.DataAccess.Mains
{
    public class MainRepository : IMainRepository
    {
        private readonly AppDbContext _db;

        public MainRepository()
        {
            _db = new AppDbContext();
        }

        public List<MainListDto> GetMainListRawData()
        {
            var mains = _db.Mains
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.Description,
                    m.CategoryId,
                    m.CreatedDate,
                    m.IsActive
                })
                .ToList();

            var approvals = _db.ApprovalProcess
                .Select(a => new
                {
                    a.MainId,
                    a.UsersId,
                    a.Status,
                    a.RequestDate
                })
                .ToList();

            var noteCounts = _db.notes
                .GroupBy(n => n.MainId)
                .Select(g => new
                {
                    MainId = g.Key,
                    Count = g.Count()
                })
                .ToList();
            var categories = _db.Categories
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .ToList();

            var result = mains.Select(m =>
            {
                var lastApproval = approvals
                    .Where(a => a.MainId == m.Id)
                    .OrderByDescending(a => a.RequestDate)
                    .FirstOrDefault();

                var noteCount = noteCounts
                    .FirstOrDefault(n => n.MainId == m.Id)?.Count ?? 0;
                var categoryName = categories
                    .FirstOrDefault(c => c.Id == m.CategoryId)?.Name;

                return new MainListDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    CategoryId = m.CategoryId,
                    CategoryName = categoryName,
                    CreatedDate = m.CreatedDate,
                    IsActive = m.IsActive,
                    NoteCount = noteCount,
                    LastApprovalUserId = lastApproval != null ? (int?)lastApproval.UsersId : null,
                    LastApprovalStatus = lastApproval != null ? lastApproval.Status : null,
                    LastApprovalDate = lastApproval != null ? (System.DateTime?)lastApproval.RequestDate : null,
                    DeleteStatus = "Yok"
                };
            }).ToList();

            return result;
        }
        public void Add(Main main)
        {
            _db.Mains.Add(main);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public OrnekDevExtremeProjesi2.Models.Main GetById(int id)
        {
            return _db.Mains.Find(id);
        }

        public void Update(OrnekDevExtremeProjesi2.Models.Main main)
        {
            _db.Entry(main).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(OrnekDevExtremeProjesi2.Models.Main main)
        {
            _db.Mains.Remove(main);
        }

        public void AddApproval(OrnekDevExtremeProjesi2.Models.ApprovalProcess approval)
        {
            _db.ApprovalProcess.Add(approval);
        }
    }
}