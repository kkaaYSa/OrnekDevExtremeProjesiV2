using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("Main")]
    public class Main
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId {  get; set; }
        public bool IsActive { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}