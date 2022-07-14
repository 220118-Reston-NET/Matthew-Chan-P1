namespace Shop.Models
{
    public partial class Inventory{
        public string? InventoryId {get; set;}
        public string? ProductId {get; set;}
        public int ProductQuantity {get; set;}
        public string? StoreId {get; set;}
        public virtual StoreFront? Stores { get; set; }
        public virtual Product? Products { get; set; }
    }
}


        // need some kind of unique store ID
        
        /*public Inventory(){
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
        */