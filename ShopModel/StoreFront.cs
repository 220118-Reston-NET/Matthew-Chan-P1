namespace ShopModel;
public class StoreFront{
    public int storeId {get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Inventory Inv { get; set; }
    public List<Order> Orders {get; set;}

    // need some kind of unique store ID
    
    public StoreFront(){
        storeId = -1;
        Name = "N/A";
        Address = "9001 Utopia Circle";
        Inv = new Inventory();
        Orders = new List<Order>{};
        
    }
    public override string ToString(){
        /*string productString = string.Join( "\n", Products); */
        string OrderString = string.Join( ",", Orders);
        return $"Name: {Name}\nStore Id{storeId}\nAddress: {Address}";
    }
}