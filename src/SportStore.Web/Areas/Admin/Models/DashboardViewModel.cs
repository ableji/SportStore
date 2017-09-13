using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int UserCount { get; set; }
        public int OrderCount { get; set; }
        public int ProductCount { get; set; }
        public int MessageCount { get; set; }

        public IEnumerable<Message> NewMessages { get; set; }


    }
}
