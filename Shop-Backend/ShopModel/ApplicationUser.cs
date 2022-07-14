using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Shop.Models
{
    public partial class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }
        public int Authority { get; set; } // 0 = prof, 1 = manager
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Profile? Profiles { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}