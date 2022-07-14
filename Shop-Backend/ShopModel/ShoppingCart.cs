using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class ShoppingCart
    {
        public string ShoppingCartId = null!;
        public string UserId = null!;
        public string ProductId = null!;
        public string StoreId = null!;
        public int Quantity; 

        public virtual ApplicationUser? ApplicationUsers { get; set; }
        public virtual Product? Products {get; set;}
        public virtual StoreFront? StoreFronts { get; set; }
    }
}