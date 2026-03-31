using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("Category")]
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Main> Main { get; set; }
    }
}