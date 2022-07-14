using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderToLineItems = new HashSet<OrderToLineItem>();
        }
        public string OrderId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? StoreId { get; set; }
        //public List<string> StoreFrontLocation {get; set;}
        //public int totalPrice { get; set; }
        public DateTime creationTime {get; set; }
        
        public virtual ApplicationUser? User { get; set; }
        public virtual StoreFront? StoreFronts { get; set; }
        public virtual ICollection<OrderToLineItem> OrderToLineItems { get; set; }
    }
}

/* Some Pseudocode
instantiate blank Order class
    loop though file
    add a LItem into the lineitem list( /// add p product, then, then add quantitiy)
    increase the total price by quantitiy + price
*/

/*
public string ToReadableFormat(){
            string orderString = "";
            int costOfOrder = 0;

            for(int i = 0; i < LineItems.Count; i ++) {
                int costOfLineItem = LineItems[i].Products.Price * LineItems[i].Quantity;
                orderString += (LineItems[i].Products.Name + " " + LineItems[i].Quantity + " = " + LineItems[i].Quantity + "*$" + LineItems[i].Products.Price + " = $" + costOfLineItem + "       Ordered from: " + StoreFrontLocation[i] + "   at datetime: " + creationTime + "\n");
                costOfOrder += costOfLineItem;
            }
            orderString += ("Total Cost: $" + costOfOrder);
            return orderString;
        }

        public override string ToString(){
            string lineItemString = string.Join( "\n", LineItems);
            string storeFrontLocationString = string.Join( "\n", StoreFrontLocation);
            //string totalPrice = string.Join( "\n", totalPrice);
            return $"Order Number: {orderNumber}\nLine Items: {lineItemString}\nStore Front Locations: {storeFrontLocationString[0]}\nTotal Price: {totalPrice}\nCreation Timestamp: {creationTime}";
        }

        public void AddItemToOrder(LineItem lItem, string sfLoc){
            LineItems.Add(lItem);
            StoreFrontLocation.Add(sfLoc);
            totalPrice += lItem.Products.Price;
        }
        */
