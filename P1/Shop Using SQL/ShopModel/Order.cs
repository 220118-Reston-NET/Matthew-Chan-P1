namespace ShopModel;
public class Order{
    public int orderNumber { get; set; }
    public List<LineItem> LineItems { get; set; }
    public List<string> StoreFrontLocation {get; set;}
    public int totalPrice { get; set; }
    public DateTime creationTime {get; set; }
    public Order(){
        orderNumber = 0;
        LineItems = new List<LineItem>{};
        StoreFrontLocation = new List<string>{};
        totalPrice = 0;
        creationTime = DateTime.Now;
    }
    
    public Order(int oId, List<LineItem> lItem, List<string> sfLoc, int tPrice){
        orderNumber = oId;
        LineItems = lItem;
        StoreFrontLocation = sfLoc;
        totalPrice = tPrice;
        creationTime = DateTime.Now;
    }
    public Order(int oId, List<LineItem> lItem, List<string> sfLoc, int tPrice, DateTime _CreationTime){
        orderNumber = oId;
        LineItems = lItem;
        StoreFrontLocation = sfLoc;
        totalPrice = tPrice;
        creationTime = _CreationTime;
    }

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
}

/* Some Pseudocode
instantiate blank Order class
    loop though file
    add a LItem into the lineitem list( /// add p product, then, then add quantitiy)
    increase the total price by quantitiy + price
*/


