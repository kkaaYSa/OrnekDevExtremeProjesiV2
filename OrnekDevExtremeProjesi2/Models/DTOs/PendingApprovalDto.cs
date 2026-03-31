using System;

namespace OrnekDevExtremeProjesi2.Models.DTOs
{
    public class PendingApprovalDto
    {
        public int Id { get; set; }
        public int MainId { get; set; }
        public string TargetTitle { get; set; }
        public string TargetDescription { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}