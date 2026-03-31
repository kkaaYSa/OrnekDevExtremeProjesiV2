using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("ApprovalProcess")]
    public class ApprovalProcess
    {
        public int Id { get; set; }

        // 1. İlişki: Main Tablosuna Bağlantı
        public int MainId { get; set; }
        [ForeignKey("MainId")]
        public virtual Main RelatedMain { get; set; }

        // 2. İlişki: Users Tablosuna Bağlantı (Kimin gönderdiği)
        // Burayı 'int' yapıyoruz çünkü Users tablosundaki 'Id' int tipinde.
        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual Users RequestingUser { get; set; }

        public DateTime RequestDate { get; set; }
        public string Status { get; set; } // "Bekliyor", "Reddedildi"
        public string AdminNote { get; set; }
    }
}