using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class ProductDto{
        
        public string ProductId {get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductDescription { get; set; } = null!;
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
    }
}

