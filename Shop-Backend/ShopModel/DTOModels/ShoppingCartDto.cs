using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class ShoppingCartDto
    {    
        public string? ShoppingCartId {get; set; }
        public string? UserId {get; set; }
        public string? ProductId {get; set; }
        public string? StoreId { get; set; }
        public int Quantity { get; set; }

    }
}

