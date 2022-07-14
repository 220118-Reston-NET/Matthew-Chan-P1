using Shop.Models;

namespace Shop.BuisnessManagement.Interfaces
{
    public interface IStoreManagementBL
    {
        Task<StoreFrontDto> AddNewStoreFront( StoreFrontDto s_storeFront );
        Task<List<StoreFrontDto>> GetAllStoreFronts();
        Task<StoreFrontDto> GetStoreFrontByName(string storeName);
        Task<StoreFrontDto> GetStoreFrontById(string storeID);
        Task<StoreFrontDto> UpdateStoreFrontInfo(StoreFrontDto storeFront);
        
        Task<InventoryDto> AddInventory(InventoryDto i_inventory);
        Task<List<InventoryDto>> GetStoreInventoryByStoreId(string storeId);
        Task<List<InventoryDto>> GetAllStoresWithProduct(string productId);
        Task<InventoryDto> UpdateInventory(string inventoryId, int amount, string updateType);
        Task<InventoryDto> RemoveInventory(string inventoryId);

        Task<ProductDto> AddNewProduct(ProductDto productDto);
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(string productId);
        Task<ProductDto> GetProductByName(string productName);
    }
}