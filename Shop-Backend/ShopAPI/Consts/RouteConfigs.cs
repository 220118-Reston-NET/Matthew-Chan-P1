namespace Shop.ShopAPI.Consts
{
    public static class RouteConfigs
    {
        //AUTHENTICATION
        public const string Register = "Register";
        public const string RegisterManager = "RegisterManager";
        public const string Login = "Login";

        //USER
        public const string Users = "Users";
        public const string Profile = "Profile";
        public const string Update = "Update";

        //STOREFRONT
        public const string StoreFronts = "StoreFront";
        public const string StoreFront = "StoreFrontId/{storeFrontId}";
        public const string StoreFrontName = "StoreFrontName/{storeFrontName}";
        public const string Inventories = "Inventory";
        public const string Inventory = "Inventory/{inventoryId}";

        public const string SearchInventory = "StoreFront/{storeFrontId}/Inventory";
        public const string FindProduct = "Product/{productId}/StoreFront";
        public const string Products = "Product";
        public const string ProductId = "ProductId/{productId}";
        public const string ProductName = "ProductName/{productName}";

        //ORDER
        public const string ShoppingCarts = "ShoppingCart";
        public const string ShoppingCart = "ShoppingCart/{shoppingCartId}";
        public const string UserShoppingCart = "ShoppingCart/User/{userId}";
        public const string Order = "Order/User/{userId}";
        public const string ShopOrder = "Order/StoreFront/{shopId}";
    }
}