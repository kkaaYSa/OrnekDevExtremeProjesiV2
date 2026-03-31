using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("UsersRole")]
    public class UsersRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; } 
        public string RoleName {  get; set; }

        
        public virtual ICollection<Users>Users  { get; set; }
    }
}