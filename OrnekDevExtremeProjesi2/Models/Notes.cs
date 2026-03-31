using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("Notes")]
    public class Notes
    {
        public int Id { get; set; }
        public int MainId { get; set; }
        [ForeignKey("MainId")]
        public virtual Main RelatedMain { get; set; }

        // 2. İlişki: Users Tablosuna Bağlantı (Kimin gönderdiği)
        // Burayı 'int' yapıyoruz çünkü Users tablosundaki 'Id' int tipinde.
        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual Users RequestingUser { get; set; }
        public string NoteText { get; set;}
        public DateTime CreatedDate { get; set; }

    }
}