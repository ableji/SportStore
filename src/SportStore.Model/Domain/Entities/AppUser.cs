using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SportStore.Model.Domain
{
    public class AppUser : IdentityUser
    {

        public int CityId { get; set; }

        public string RestOfAddress { get; set; }


        public virtual IList<Order> Orders { get; set; }
    }
}
