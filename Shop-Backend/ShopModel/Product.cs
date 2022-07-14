using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class Product{
        
        public Product()
        {
            LineItems = new HashSet<LineItem>();
            Inventories = new HashSet<Inventory>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }
        public string ProductId {get; set; } = null!;
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string? ProductDescription { get; set; } = null!;
        private int _productAgeRestriction;
        public int ProductAgeRestriction { 
            get{
                return _productAgeRestriction;
            } 
            set{
                if(value < 0){
                    throw new Exception("Error. Age restriction cannot be less than 0");
                }
                _productAgeRestriction = value;
            }
        }

        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }
        public virtual ICollection<LineItem>? LineItems {get; set; }

    }
}

/*
public Product(){
        
            prodId = -1;
            Name = "Nothing";
            Price = 1;
            Desc = "DNE";
            Age_Restriction = 1;
        }
        
        public Product(string productName, int productPrice){
            prodId = -1;
            Name = productName;
            Price = productPrice;
            Desc = "";
            Age_Restriction = 1;
        }
        public Product(int productId, string productName, int productPrice, string productDescription, int productAgeRestriction){
            prodId = productId;
            Name = productName;
            Price = productPrice;
            Desc = productDescription;
            Age_Restriction = productAgeRestriction;
        }
        public override string ToString(){
            return $"Product Name: {Name}\nPrice: {Price}\nDescription: {Desc}\nAge Restriction: {Age_Restriction}";
        }
        */