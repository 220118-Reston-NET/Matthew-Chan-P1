namespace Shop.Models
{
    public partial class InventoryDto{
        public string? InventoryId {get; set;}
        public string? StoreId {get; set;}
        public string? ProductId {get; set;}
        public int ProductQuantity {get; set;}
    }
}
