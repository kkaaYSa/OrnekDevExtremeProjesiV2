using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrnekDevExtremeProjesi2.Models
{
    [Table("Users")]
    public class Users
    {
       public int Id { get; set; }
       public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateDate { get; set; }

        public Boolean IsActive { get; set; }

        [ForeignKey("RoleId")]
        public virtual UsersRole UsersRole { get; set; }
    }
}