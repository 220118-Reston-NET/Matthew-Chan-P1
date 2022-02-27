namespace ShopModel;
public class Inventory{
    public List<Product> Products {get; set;}
    //Dictionary < Product, int > quantity;
    public List<int> quantity {get; set;}
    public int storeId {get; set;}

    // need some kind of unique store ID
    
    public Inventory(){
        Products = new List<Product>{};
        //quantity = new Dictionary<Product, int>();
        quantity = new List<int>{};
        storeId = 0;
    }

    public Inventory(List<Product> prod, List<int> quant, int sId){
        Products = prod;
        //quantity = new Dictionary<Product, int>();
        quantity = quant;
        storeId = sId;

    }

    public override string ToString(){
        string productString = string.Join( "\n", Products);

        return $"Products: {productString}\n";
    }


    
}