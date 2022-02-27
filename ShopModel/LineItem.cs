namespace ShopModel;
public class LineItem{
    public int lineItemId {get; set; }
    public Product Products { get; set; }
    public int Quantity { get; set; }
    public int TotalPrice {get; set;}
    
    public LineItem(){
        lineItemId = 0;
        Products = new Product();
        Quantity = 0;
        TotalPrice = 0;
    }

    public LineItem(Product p, int q){
        lineItemId = -1;
        Products = p;
        Quantity = q;
        TotalPrice = 0;
        calcTotalPrice();
    }

    public LineItem(int lId, Product p, int q){
        lineItemId = lId;
        Products = p;
        Quantity = q;
        TotalPrice = 0;
        calcTotalPrice();
    }
    
    public void calcTotalPrice(){
        TotalPrice = Products.Price * Quantity;
    }

    public override string ToString(){
        return $"{Products}\nQuantity: {Quantity}";
    }
} 