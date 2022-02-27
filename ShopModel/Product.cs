namespace ShopModel;
public class Product{
    
    public int prodId {get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Desc { get; set; }
    private int _ageRestriction;
    public int Age_Restriction { 
        get{
            return _ageRestriction;
        } 
        set{
            if(value < 0){
                throw new Exception("Error. Age restriction cannot be less than 0");
            }
            _ageRestriction = value;
        }
    }





    public Product(){
    
        prodId = -1;
        Name = "Nothing";
        Price = 1;
        Desc = "DNE";
        Age_Restriction = 1;
    }
    
    public Product(string productName, int productPrice){
        prodId = -1;
        Name = productName;
        Price = productPrice;
        Desc = "";
        Age_Restriction = 1;
    }
    public Product(int productId, string productName, int productPrice, string productDescription, int productAgeRestriction){
        prodId = productId;
        Name = productName;
        Price = productPrice;
        Desc = productDescription;
        Age_Restriction = productAgeRestriction;
    }
    public override string ToString(){
        return $"Product Name: {Name}\nPrice: {Price}\nDescription: {Desc}\nAge Restriction: {Age_Restriction}";
    }
}