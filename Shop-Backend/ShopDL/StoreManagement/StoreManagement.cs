using Microsoft.EntityFrameworkCore;
using Shop.DatabaseManagement.Interfaces;
using Shop.Models;

namespace Shop.DatabaseManagement.Implements
{
    public class StoreManagementDL : IStoreManagementDL
    {
        private readonly ShopContext _context;

        public StoreManagementDL(ShopContext context)
        {
            _context = context;
        }
        public async Task<StoreFrontDto> AddStoreFront(StoreFrontDto s_storeFrontDto)
        {
            StoreFront storeFront = StoreFrontDtoToStoreFront(s_storeFrontDto);
            await _context.StoreFronts.AddAsync(storeFront);
            await _context.SaveChangesAsync();
            Console.WriteLine(storeFront.StoreId );
            return StoreFrontToDto(storeFront);
        }

        public async Task<List<StoreFrontDto>> GetAllStoreFronts()
        {
            List<StoreFrontDto> _result = await _context.StoreFronts
                                                        .Select(s => new StoreFrontDto()
                                                        {
                                                            StoreId = s.StoreId,
                                                            StoreName = s.StoreName,
                                                            StoreAddress = s.StoreAddress
                                                        }).ToListAsync();
            if (!_result.Any())
            {
                throw new Exception("There are no StoreFronts in the database");
            }
            else
            {
                return _result;
            }
        }

        public async Task<StoreFrontDto> GetStoreFrontByName(string storeName)
        {
            StoreFrontDto? _result = await _context.StoreFronts
                                                    .Select(s => new StoreFrontDto()
                                                    {
                                                        StoreId = s.StoreId,
                                                        StoreName = s.StoreName,
                                                        StoreAddress = s.StoreAddress
                                                    }).FirstOrDefaultAsync(s => s.StoreName == storeName);
            if (_result == null)
            {
                throw new Exception("Store Front Name DNE");
            }
            else
            {
                return _result;
            }                         
        }

        public async Task<StoreFrontDto> UpdateStoreFrontInfo(StoreFrontDto s_storeFrontDto)
        {
            StoreFrontDto? _result = await _context.StoreFronts
                                                    .Select( s => new StoreFrontDto()
                                                    {
                                                        StoreId = s.StoreId,
                                                        StoreName = s.StoreName,
                                                        StoreAddress = s.StoreAddress
                                                    }).FirstOrDefaultAsync(s => s.StoreId == s_storeFrontDto.StoreId);
            if (_result == null)
            {
                throw new Exception("Store Front DNE");
            }
            else
            {
                return _result;
            }          
        }

        public async Task<InventoryDto> AddInventory(InventoryDto i_inventory)
        {
            Inventory inventory = InventoryDtoToInventory(i_inventory);
            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return InventoryToDto(inventory);
        }
        public async Task<List<InventoryDto>> GetStoreInventory(string storeId)
        {
            List<InventoryDto>? _result = await _context.Inventories
                                                        .Where(s => s.StoreId.Equals(storeId))
                                                        .Select(s => new InventoryDto()
                                                        {
                                                            InventoryId = s.InventoryId,
                                                            StoreId = s.StoreId,
                                                            ProductId = s.ProductId,
                                                            ProductQuantity = s.ProductQuantity,
                                                        }).ToListAsync();
            
            if (_result == null)
            {
                throw new Exception("Inventory DNE");
            }
            else
            {
                return _result;
            }          
        }
        public async Task<List<InventoryDto>> GetAllStoresWithProduct(string productId)
        {
            List<InventoryDto>? _result = await _context.Inventories
                                                        .Where(s => s.ProductId.Equals(productId))
                                                        .Select(s => new InventoryDto()
                                                        {
                                                            InventoryId = s.InventoryId,
                                                            StoreId = s.StoreId,
                                                            ProductId = s.ProductId,
                                                            ProductQuantity = s.ProductQuantity,
                                                        }).ToListAsync();
            if (_result == null)
            {
                throw new Exception("Profile DNE");
            }
            else
            {
                return _result;
            }   
        }

        public async Task<ProductDto> AddProduct(ProductDto product)
        {
            Product p_product = ProductDtoToProduct(product);
            //p_prof.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            await _context.Products.AddAsync(p_product);
            await _context.SaveChangesAsync();

            return ProductToDto(p_product);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            List<ProductDto> _result = await _context.Products
                                                    .Select(p => new ProductDto()
                                                    {
                                                        ProductId = p.ProductId,
                                                        ProductName = p.ProductName,
                                                        ProductPrice = p.ProductPrice,
                                                        ProductDescription = p.ProductDescription,
                                                        ProductAgeRestriction = p.ProductAgeRestriction
                                                    }).ToListAsync();

            if (!_result.Any())
            {
                throw new Exception("Product DNE");
            }
            else
            {
                return _result;
            }
        }

        public async Task<InventoryDto> IncreaseInventory(string inventoryId, int increasedAmount)
        {
            Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
            if(inventory == null)
                throw new Exception("Inventory DNE");
            inventory.ProductQuantity += increasedAmount;
            await _context.SaveChangesAsync();
            return InventoryToDto(inventory);
        }
        
        public async Task<InventoryDto> DecreaseInventory(string inventoryId, int decreasedAmount)
        {
            Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
            if(inventory == null)
                throw new Exception("Inventory DNE");
            inventory.ProductQuantity -= decreasedAmount;
            await _context.SaveChangesAsync();
            return InventoryToDto(inventory);
        }
        public async Task<InventoryDto> DecreaseInventory(string storeId, string productId, int decreasedAmount)
        {
            Inventory? inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.StoreId == storeId && i.ProductId == productId);
            if(inventory == null)
                throw new Exception("Inventory DNE");
            inventory.ProductQuantity -= decreasedAmount;
            await _context.SaveChangesAsync();
            return InventoryToDto(inventory);
            
        }
        
        public async Task<InventoryDto> RemoveInventory(string inventoryId)
        {
            Inventory? inventoryToRemove = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
            if (inventoryToRemove != null) {
                _context.Inventories.Remove(inventoryToRemove);
                await _context.SaveChangesAsync();
                return InventoryToDto(inventoryToRemove);
            }
            else
            {
                throw new Exception("Error. Inventory not found. Inventory Could not be deleted");
            }
            
        }

        public async Task<ProductDto> GetProductById(string productId)
        {
            ProductDto? _result = await _context.Products
                                                .Select(p => new ProductDto()
                                                {
                                                    ProductId = p.ProductId,
                                                    ProductName = p.ProductName,
                                                    ProductPrice = p.ProductPrice,
                                                    ProductDescription = p.ProductDescription,
                                                    ProductAgeRestriction = p.ProductAgeRestriction
                                                }).FirstOrDefaultAsync(p => p.ProductId == productId);
            if (_result == null)
            {
                throw new Exception("Product DNE");
            }
            else
            {
                return _result;
            }
        }

        public async Task<ProductDto> GetProductByName(string productName)
        {
            ProductDto? _result = await _context.Products
                                                .Select(p => new ProductDto()
                                                {
                                                    ProductId = p.ProductId,
                                                    ProductName = p.ProductName,
                                                    ProductPrice = p.ProductPrice,
                                                    ProductDescription = p.ProductDescription,
                                                    ProductAgeRestriction = p.ProductAgeRestriction
                                                }).FirstOrDefaultAsync(p => p.ProductName == productName);
            if (_result == null)
            {
                throw new Exception("Product DNE");
            }
            else
            {
                return _result;
            }
        }

        private StoreFrontDto StoreFrontToDto(StoreFront s_storeFront)
        {
            StoreFrontDto _storeFrontDto = new StoreFrontDto()
            {
                StoreId = s_storeFront.StoreId,
                StoreName = s_storeFront.StoreName,
                StoreAddress = s_storeFront.StoreAddress
            };
            return _storeFrontDto;
        }
        private StoreFront StoreFrontDtoToStoreFront(StoreFrontDto s_storeFrontDto)
        {
            StoreFront _storeFront = new StoreFront()
            {
                StoreId = s_storeFrontDto.StoreId,
                StoreName = s_storeFrontDto.StoreName,
                StoreAddress = s_storeFrontDto.StoreAddress
            };
            return _storeFront;
        }

        private InventoryDto InventoryToDto(Inventory i_inventory)
        {
            InventoryDto _storeFrontDto = new InventoryDto()
            {
                InventoryId = i_inventory.InventoryId,
                StoreId = i_inventory.StoreId,
                ProductId = i_inventory.ProductId,
                ProductQuantity = i_inventory.ProductQuantity
            };
            return _storeFrontDto;
        }
        private Inventory InventoryDtoToInventory(InventoryDto i_inventoryDto)
        {
            Inventory _storeFront = new Inventory()
            {
                InventoryId = i_inventoryDto.InventoryId,
                StoreId = i_inventoryDto.StoreId,
                ProductId = i_inventoryDto.ProductId,
                ProductQuantity = i_inventoryDto.ProductQuantity
            };
            return _storeFront;
        }
        private ProductDto ProductToDto(Product p_product)
        {
            ProductDto _productDto = new ProductDto()
            {
                ProductId = p_product.ProductId,
                ProductName = p_product.ProductName,
                ProductPrice = p_product.ProductPrice,
                ProductDescription = p_product.ProductDescription,
                ProductAgeRestriction = p_product.ProductAgeRestriction
            };
            return _productDto;
        }
        private Product ProductDtoToProduct(ProductDto p_productDto)
        {
            Product _product = new Product()
            {
                ProductId = p_productDto.ProductId,
                ProductName = p_productDto.ProductName,
                ProductPrice = p_productDto.ProductPrice,
                ProductDescription = p_productDto.ProductDescription,
                ProductAgeRestriction = p_productDto.ProductAgeRestriction
            };
            return _product;
        }
    }
}