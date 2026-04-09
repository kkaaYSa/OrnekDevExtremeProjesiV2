using System;

namespace OrnekDevExtremeProjesi2.Models.DTOs
{
    public class MainListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public int NoteCount { get; set; }
        public string AdminNote { get; set; }

        public int? LastApprovalUserId { get; set; }
        public string LastApprovalStatus { get; set; }
        public DateTime? LastApprovalDate { get; set; }

        public string DeleteStatus { get; set; }
    }
}