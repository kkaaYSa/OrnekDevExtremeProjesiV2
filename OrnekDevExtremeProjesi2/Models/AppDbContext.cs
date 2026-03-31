using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrnekDevExtremeProjesi2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=OrnekConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
        

        public DbSet<Main> Mains { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersRole> UsersRole { get; set; }
        public DbSet<ApprovalProcess> ApprovalProcess { get; set; }
        public DbSet<Notes> notes { get; set; }
    }

}