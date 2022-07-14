using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class LineItem{

        public LineItem()
        {
            OrdersToLineItems = new HashSet<OrderToLineItem>();
        }
        public string? LineItemId {get; set; }
        public string? ProductId {get; set;}
        public int ItemQuantity { get; set; }
        public int TotalPrice {get; set;}   // ??????????     
        public virtual Product? Products { get; set; }
        public virtual ICollection<OrderToLineItem>? OrdersToLineItems { get; set; }
    } 
}