using Shop.Models;

namespace Shop.DatabaseManagement.Interfaces
{
    public interface IStoreManagementDL
    {
        Task<StoreFrontDto> AddStoreFront(StoreFrontDto storeFront);
        Task<List<StoreFrontDto>> GetAllStoreFronts();
        Task<StoreFrontDto> GetStoreFrontByName(string storeName);
        Task<StoreFrontDto> UpdateStoreFrontInfo(StoreFrontDto storeFront);
        
        Task<InventoryDto> AddInventory(InventoryDto i_inventory);
        Task<InventoryDto> IncreaseInventory(string inventoryId, int increasedAmount);
        Task<InventoryDto> DecreaseInventory(string inventoryId, int amount);
        Task<InventoryDto> DecreaseInventory(string storeId, string productId, int decreasedAmount);
        Task<InventoryDto> RemoveInventory(string  inventoryId);
        Task<List<InventoryDto>> GetStoreInventory(string storeId);
        Task<List<InventoryDto>> GetAllStoresWithProduct(string productId);

        Task<ProductDto> AddProduct(ProductDto p_prod);

        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(string productId);
        Task<ProductDto> GetProductByName(string productName);

    }
}