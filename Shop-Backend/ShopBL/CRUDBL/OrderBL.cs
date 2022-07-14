// using CRUDShopDL;
// using ShopModel;


// namespace ShopBL{
//     public class OrderBL: IOrderBL {
//         // Dependency Injection Pattern
//         // ==================================
//         private IOrderRepository _repo;

//         public OrderBL(IOrderRepository o_repo){
//             _repo = o_repo;
//         }
//         // ==================================

        
        
//         public Order AddOrder(Order o_order, int profId, int storeId){
//             // i need to make sure the order doesn't exceed inventory
//             CheckValidCId(profId);
//             foreach(LineItem li in o_order.LineItems){
//                 CheckValidProduct(storeId, li.Products.prodId);
//             }


            
//             Inventory storeInventory = GetSpecificInventory(storeId);
//             // then get all existing order
//             //now, find the index of the order and find the index of the inventory
            
//             // then add up the items to see if order would exceed the inventory
//             if(storeInventory.quantity[0] < o_order.LineItems[0].Quantity ){
//                 throw new Exception("Exceeing capacity exception");
//             }
//             // if there's an issue, return an error message
//             // if there's no error, just do the code below
//             return _repo.AddOrder(o_order, profId, storeId);
//         } 

//         public Order AddOrder(){
//             return _repo.AddOrder();
//         } 
        

//         public Product ProductIdToProduct(int prodId){
//             return _repo.ProductIdToProduct(prodId);
//         }
        
//         /*
//         public Order GetOrderByCustId(int o_ordId){
//             return _repo.GetOrderByCustId(o_ordId);
//         }
//         */
//         public List<Order> GetAllOrder(){
//             List<Order> listOfAllOrders = _repo.GetAllOrder();
//             if(!listOfAllOrders.Any())
//             {
//                 throw new Exception("Error, no orders to view");
//             }
//             return _repo.GetAllOrder();
//         }
//         public List<Order> GetAProfileOrder( int cId){
//             CheckValidCId(cId);
//             List<Order> listOfCustOrders = _repo.GetAProfileOrder(cId);
//             if(!listOfCustOrders.Any())
//             {
//                 throw new Exception("Error, no orders to view for this Profile");
//             }
//             return listOfCustOrders;
//         }

//         public List<Order> GetAShopOrder( int sId) {
//             CheckValidStoreId(sId);
//             List<Order> listOfShopOrders = _repo.GetAShopOrder(sId);
//             if(!listOfShopOrders.Any())
//             {
//                 throw new Exception("Error, no orders to view for this shop");
//             }
//             return listOfShopOrders;
//         }

//         public bool CheckValidProduct(int sId, int pId){
//             Inventory inv = GetSpecificInventory(sId);
//             for(int i = 0; i < inv.Products.Count; i++ ){
//                 if(inv.Products[i].prodId == pId ){
//                     return true;
//                 }
//             }
//             throw new Exception("Product Not Valid");
//         }

        
//         public bool CheckValidCId(int cId){
//             List<Profile> tempCust = _repo.GetAllProfile();
//             for(int i = 0; i < tempCust.Count; i++){
//                 if(tempCust[i].profId == cId){
//                     return true;
//                 }
//             }
//             throw new Exception("The Profile Id is not valid");

//         }

//         public bool checkOrder(int prodId, int quantity, Order Orders, int sId ){
//             // add the quantity of the list of orders and quantity
//             int totalOrderQuantity = 0;
//             for( int i = 0; i < Orders.LineItems.Count; i++){
//                 if(Orders.LineItems[i].Products.prodId == prodId){
//                     totalOrderQuantity += Orders.LineItems[i].Quantity; // accounts for the items in the order
//                 }
//             }
//             totalOrderQuantity += quantity; // this is to account for the items profomer wants to order
//             // get the shop inventory
//             Inventory inv = GetSpecificInventory(sId);
//             int inventoryQuantity = 0;
//             for( int i = 0; i < inv.Products.Count; i++){
//                 if(inv.Products[i].prodId == prodId){
//                     inventoryQuantity = inv.quantity[i];
//                     break;
//                 }
//             }
//             // compare
//             if(totalOrderQuantity <= inventoryQuantity){
//                 return true;
//             }
//             else{
//                 throw new Exception("Exceeding inventory. Order is not valid");
//             }
//         }

//         /*


//         public bool CheckIfEmpty(List<Order> listOfCust){
//             if(listOfCust.Any() == false){
//                 return true;
//             }
//             else{
//                 return false;
//             }
            
//         } */


//         ////////////

//         public string ConvertSFIdToSFAddress(int sId){
//             return _repo.StoreFrontIdToAddress(sId);
//         }
//         ///////
//         public Inventory GetSpecificInventory(int id){
//             Inventory specInventory = _repo.GetAnInventory(id);
//             return specInventory;
//         }

//         public void printProductsInInventory(Inventory inv){
//             for(int i = 0; i < inv.Products.Count; i++) {
//                 Console.WriteLine(inv.Products[i].prodId + " " + inv.Products[i].Name + ": " + inv.quantity[i]);
//             }
//         }
//         public void printProductsInInventory(Inventory inv, Order ord){
//             List<int> filler = new List<int>{};
//             for(int i = 0; i < inv.Products.Count; i++){
//                 filler.Add(0);
//                 for(int j = 0; j < ord.LineItems.Count; j++){
//                     if(ord.LineItems[j].Products.prodId == inv.Products[i].prodId){
//                         filler[i] += ord.LineItems[j].Quantity;
//                     }
//                 }
//             }

//             for(int i = 0; i < inv.Products.Count; i++) {
//                 Console.WriteLine(inv.Products[i].prodId + " " + inv.Products[i].Name + ": " + inv.quantity[i] + " - " + filler[i]);
//             }
//         }
//         public bool CheckValidAge(int profId,int prodId){
//             List<Profile> prof = _repo.GetAllProfile();
//             List<Product> listOfProducts = _repo.GetAllProducts();
//             int reqAge = 0;
//             for(int i = 0; i < listOfProducts.Count;i++){
//                 if(listOfProducts[i].prodId == prodId){
//                     reqAge = listOfProducts[i].Age_Restriction;
//                     break;
//                 }
//             }
//             for(int i = 0; i < prof.Count; i++){
//                 if(prof[i].profId == profId && prof[i].Age > reqAge){
//                     return true;
//                 }
//             }
//             throw new Exception("Profile is too young to buy this product. Please try again");
//         }
//         public bool CheckValidStoreId(int sId){
//             List<StoreFront> tempSF = _repo.GetAllStoreFront();
//             for(int i = 0; i < tempSF.Count; i++){
//                 if(tempSF[i].storeId == sId){
//                     return true;
//                 }
//             }
//             throw new Exception("The Profile Id is not valid");
//         }

//         public void DisplayAllStoreFronts(){
//             List<StoreFront> listOfStoreFronts = _repo.GetAllStoreFront();
//             for(int i = 0; i < listOfStoreFronts.Count; i++){
//                 Console.WriteLine(listOfStoreFronts[i].storeId + ") " + listOfStoreFronts[i].Name);
//             }
//         }
        
//         public string DisplayAllOrdersInReadableFormat(List<Order> o){
//             string orderString = "";
//             int costOfOrder = 0;
//             for(int i = 0; i < o.Count; i++){
//                 for(int j = 0; j < o[i].LineItems.Count; j ++) {
//                     int costOfLineItem = o[i].LineItems[i].Products.Price * o[i].LineItems[i].Quantity;
//                     orderString += (o[i].LineItems[i].Products.Name + " " + o[i].LineItems[i].Quantity + " = " + o[i].LineItems[i].Quantity + "*$" + o[i].LineItems[i].Products.Price + " = $" + costOfLineItem + "       Ordered from: " + o[i].StoreFrontLocation[i] + "   at datetime: " + o[i].creationTime + "\n");
//                     costOfOrder += costOfLineItem;
//                     break;
//                 }
                
//             }
//             orderString += ("Total Cost: $" + costOfOrder);
//             return orderString;
//         }

//         public Order AddCart(Order o_order, int profId, int storeId){
//             Inventory storeInventory = GetSpecificInventory(storeId);
//             if(storeInventory.quantity[0] < o_order.LineItems[0].Quantity ){
//                 throw new Exception("Exceeing capacity exception");
//             }
//             return _repo.AddCart(o_order, profId, storeId);
//         }
//         public Order GetAllCart(){
//             return _repo.GetAllCart();
//         }
//         public void ClearCart(){
//             _repo.ClearCart();
//         }

//     } 


    

// }   