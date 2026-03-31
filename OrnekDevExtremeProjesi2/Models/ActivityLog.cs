using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table ("ActivityLogs")]
    public class ActivityLog
    {
        [Key]
        public int Id {  get; set; }
        public int MainId { get; set; }
        [Required, StringLength(50)]
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual Users RequestingUser { get; set; }


    }
}