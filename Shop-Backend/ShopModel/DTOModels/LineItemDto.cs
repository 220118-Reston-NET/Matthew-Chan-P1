using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class LineItemDto{
        public string? LineItemId {get; set; }
        public string? ProductId {get; set;}
        public int ItemQuantity { get; set; }
        public int TotalPrice {get; set;}
    } 
}