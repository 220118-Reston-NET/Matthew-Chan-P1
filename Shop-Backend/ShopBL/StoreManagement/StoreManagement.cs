using Shop.Models;
using Shop.BuisnessManagement.Interfaces;
using Shop.DatabaseManagement.Interfaces;

namespace Shop.BuisnessManagement.Implements
{
    public class StoreManagementBL : IStoreManagementBL
    {
        private readonly IStoreManagementDL _repo;
        public StoreManagementBL(IStoreManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<StoreFrontDto> AddNewStoreFront( StoreFrontDto s_storeFront )
        {
            return await _repo.AddStoreFront(s_storeFront);
        }
        public async Task<List<StoreFrontDto>> GetAllStoreFronts()
        {
            try
            {
                return await _repo.GetAllStoreFronts();
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<StoreFrontDto> GetStoreFrontByName(string storeName)
        {
            try
            {
                return await _repo.GetStoreFrontByName(storeName);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<StoreFrontDto> GetStoreFrontById(string storeId)
        {
            try
            {
                List<StoreFrontDto> _result =  await _repo.GetAllStoreFronts();
                return _result.Find(store => store.StoreId.Equals(storeId));
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<StoreFrontDto> UpdateStoreFrontInfo(StoreFrontDto storeFront)
        {
            try
            {
                return await _repo.UpdateStoreFrontInfo(storeFront);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<InventoryDto> AddInventory(InventoryDto i_inventory)
        {
            return await _repo.AddInventory(i_inventory);
        }
        public async Task<List<InventoryDto>> GetStoreInventoryByStoreId(string storeId)
        {
            try
            {
                return await _repo.GetStoreInventory(storeId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<List<InventoryDto>> GetAllStoresWithProduct(string productId)
        {
            try
            {
                return await _repo.GetAllStoresWithProduct(productId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<InventoryDto> UpdateInventory(string inventoryId, int amount, string updateType)
        {
            try
            {
                if(updateType.ToLower() == "increase")
                    return await _repo.IncreaseInventory(inventoryId, amount);
                else if(updateType.ToLower() == "decrease")
                    return await _repo.DecreaseInventory(inventoryId, amount);
                else
                    throw new Exception("Transaction Type not valid. Only acceptable transactions are: (increase/decrease)");
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<InventoryDto> RemoveInventory(string inventoryId)
        {
            try
            {
                return await _repo.RemoveInventory(inventoryId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }


        public Task<ProductDto> AddNewProduct(ProductDto productDto)
        {
            try
            {
                GetProductByName(productDto.ProductName);
                return _repo.AddProduct(productDto);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Task<List<ProductDto>> GetAllProducts()
        {
            try
            {
                return _repo.GetAllProducts();
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Task<ProductDto> GetProductById(string productId)
        {
            try
            {
                return _repo.GetProductById(productId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }
        public Task<ProductDto> GetProductByName(string productName)
        {
            try
            {
                return _repo.GetProductByName(productName);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }
        
    }
}