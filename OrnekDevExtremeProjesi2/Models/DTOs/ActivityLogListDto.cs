using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekDevExtremeProjesi2.Models.DTOs
{
    public class ActivityLogListDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
    }
}