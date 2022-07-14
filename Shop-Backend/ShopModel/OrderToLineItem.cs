namespace Shop.Models
{
    public class OrderToLineItem
    {
        public string OrderToLineItemId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? LineItemId{ get; set; }
        
        public virtual Order? Orders { get; set; }
        public virtual LineItem? LineItems {get; set; }
        
    }
}
