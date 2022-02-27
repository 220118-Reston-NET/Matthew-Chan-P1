
using ShopDL;
using ShopModel;


namespace ShopBL{
    public class StoreFrontBL: IStoreFrontBL {
        // Dependency Injection Pattern
        // ==================================
        private IStoreFrontRepository _repo;
        
        public StoreFrontBL(IStoreFrontRepository s_repo){
            _repo = s_repo;
        }

        
        // ==================================

        public StoreFront AddStoreFront(StoreFront c_StoreFront){
            Random rand = new Random();

            //c_StoreFront.uniqueID == rand.Next(0,9999999); will determine a way to actually make it unique later, or should just do some kind of incrmeent with a static variable would work too

            return _repo.AddStoreFront(c_StoreFront);
        }

        public List<StoreFront> GetAllStoreFronts(){
            return _repo.GetAllStoreFront();
        }

        public List<Product> GetAllProducts(){
            return _repo.GetAllProducts();
        }


        
        public List<StoreFront> SearchStoreFrontName(string s_name){
            List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
            // LINQ library
            return listOfStoreFronts
                        .Where(store => store.Name.Contains(s_name))
                        .ToList();
        }
        
        public List<StoreFront> SearchStoreFrontProducts(string s_product){
            List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
            // LINQ library
            List<StoreFront> listOfStoreFrontWithProduct = new List<StoreFront>{};

            for(int i = 0; i < listOfStoreFronts.Count; i++){
                for(int j = 0; j < listOfStoreFronts[i].Inv.Products.Count; j++){
                    if(listOfStoreFronts[i].Inv.Products[j].Name == s_product ){
                        listOfStoreFrontWithProduct.Add(listOfStoreFronts[i]);
                    }
                }
            }
            
            
            return listOfStoreFrontWithProduct;
                        //.Where(store => store.Inv.Products.Contains(s_product))
                        //.ToList()); 
        }


        public void DisplayAllStoreFronts(){
            List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
            for(int i = 0; i < listOfStoreFronts.Count; i++){
                Console.WriteLine(listOfStoreFronts[i].storeId + ") " + listOfStoreFronts[i].Name);
            }
        }

        
        public bool CheckValidStoreId(int storeId){
            List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
            for(int i = 0; i < listOfStoreFronts.Count;i++){
                Console.WriteLine("comapring with ID: " + listOfStoreFronts[i].storeId);
                if(listOfStoreFronts[i].storeId == storeId){
                    return true;
                }
                
            }
            throw new Exception("Error: not a valid store"); // or should i throw an error?????????
        }

        public List<StoreFront> checkStoresForAProduct(int pId){
            List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
            List<StoreFront> listOfStoreFrontsWithProduct = new List<StoreFront>{};

            for(int i = 0; i < listOfStoreFronts.Count; i++){
                for(int j = 0; j < listOfStoreFronts[i].Inv.Products.Count; j++){
                    if(listOfStoreFronts[i].Inv.Products[j].prodId == pId){
                        listOfStoreFrontsWithProduct.Add(listOfStoreFronts[i]);
                    }
                }
            }
            return listOfStoreFrontsWithProduct;
        }

        
        

        

        public bool CheckValidProduct(int pId){
            List<Product> allProducts = _repo.GetAllProducts();

            for(int i = 0; i < allProducts.Count; i++){
                if(allProducts[i].prodId == pId){
                    return true;
                }
            }
            throw new Exception("This product is not Valid");
        }


        public bool CheckIfEmpty(List<StoreFront> listOfStores){
            if(listOfStores.Any() == false){
                return true;
            }
            else{
                return false;
            }
        }

        public List<Inventory> GetAllInventory(){
            return _repo.GetAllInventory();
        }

        public Inventory GetSpecificInventory(int id){
            Inventory specInventory = _repo.GetAnInventory(id);
            return specInventory;
        }

        public Inventory AddOrRestock(int p_prodId, int s_storeId, int amount){
            CheckValidProductId(p_prodId);
            CheckValidStoreId(s_storeId);
            Inventory specInventory = _repo.GetAnInventory(s_storeId);
            foreach(Product prod in specInventory.Products){
                if(prod.prodId == p_prodId){
                    return (_repo.RestockInventory(p_prodId,s_storeId,amount));
                }
            }
            return _repo.AddItemToInventory(p_prodId, s_storeId, amount);
        }

        public void printProductsInInventory(Inventory inv){
            for(int i = 0; i < inv.Products.Count; i++) {
                Console.WriteLine(inv.Products[i].Name + ": " + inv.quantity[i] + " - $" + inv.Products[i].Price);
            }
        }
    
        public void RestockInventory(int p_prodId, int s_storeId, int amount){
            // need to check if prod and store and amount are valid
            
            //now update inventory
            _repo.RestockInventory(p_prodId, s_storeId, amount);
        }

        public Inventory AddItemToInventory(int p_prodId, int s_storeId, int amount){
            return _repo.AddItemToInventory(p_prodId, s_storeId, amount);
        }
        /*public List<Product> GetProductsFromShopId (int sId) {
            List<Products> = _
        }*/



        public void DisplayAllProducts (){
            List<Product> listOfProducts = _repo.GetAllProducts();
            for(int i = 0; i < listOfProducts.Count; i++){
                Console.WriteLine(listOfProducts[i].prodId + ") " + listOfProducts[i].Name);
            }
        }


        public bool CheckValidProductId(int prodId){
            List<Product> listOfProducts = _repo.GetAllProducts();
            for(int i = 0; i < listOfProducts.Count;i++){
                if(listOfProducts[i].prodId == prodId){
                    return true;
                }
                
            }
            throw new Exception("This product is not Valid for this store");
        }
        

        public bool CheckValidProductInStore(int sId,int pId){
            Inventory inv = GetSpecificInventory(sId);
            for(int i = 0; i < inv.Products.Count; i++){
                if(inv.Products[i].prodId == pId){
                    return true;
                }
            }
            throw new Exception("Product DNE in this store");
        }
        

    }
}
