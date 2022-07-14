using System;
using System.Collections.Generic;
namespace Shop.Models
{
    public class StoreFront{
        public string? StoreId {get; set; }
        public string? StoreName { get; set; }
        public string? StoreAddress { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<Order>? Orders {get; set;}
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }
    }
}

/*
    public StoreFront(){
        storeId = -1;
        Name = "N/A";
        Address = "9001 Utopia Circle";
        Inv = new Inventory();
        Orders = new List<Order>{};
        
    }
    public override string ToString(){
        string productString = string.Join( "\n", Products); 
        string OrderString = string.Join( ",", Orders);
        return $"Name: {Name}\nStore Id{storeId}\nAddress: {Address}";
    }
*/