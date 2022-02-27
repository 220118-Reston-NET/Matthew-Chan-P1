using ShopDL;
using ShopModel;


namespace ShopBL{
    public class ProductBL: IProductBL {
        // Dependency Injection Pattern
        // ==================================
        private IProductRepository _repo;

        public ProductBL(IProductRepository p_repo){
            _repo = p_repo;
        }
        // ==================================

        
        
        public Product AddProduct(Product p_product){

            return _repo.AddProduct(p_product);
        } 
        
        /*
        public Product GetProductByCustId(int p_prodId){
            return _repo.GetProductByCustId(p_prodId);
        }
        */
        public List<Product> GetAllProducts(){
            return _repo.GetAllProducts();
        }
        /*

        public List<Product> SearchProductFromCustId(int p_Id){
            List<Product> listOfProducts = _repo.GetAllProduct();
            // LINQ library
            return listOfProducts
                        .Where(prod => prod.prodId == p_Id)
                        .ToList();
        }
        public List<Product> SearchProduct(string p_name){
            List<Product> listOfProducts = _repo.GetAllProduct();
            // LINQ library
            return listOfProducts
                        .Where(prod => prod.Name.Contains(p_name))
                        .ToList();
        }
        
        public List<Product> SearchProductFromNumber(string p_pnum){
            List<Product> listOfProducts = _repo.GetAllProduct();
            // LINQ library
            return listOfProducts
                        .Where(prod => prod.PhoneNumber.Contains(p_pnum))
                        .ToList();
        }
        public List<Product> SearchProductFromEMail(string p_email){
        List<Product> listOfProducts = _repo.GetAllProduct();
        // LINQ library
        return listOfProducts
                    .Where(prod => prod.Email.Contains(p_email))
                    .ToList();
        }


        public bool CheckIfEmpty(List<Product> listOfCust){
            if(listOfCust.Any() == false){
                return true;
            }
            else{
                return false;
            }
            
        } */
    }
}