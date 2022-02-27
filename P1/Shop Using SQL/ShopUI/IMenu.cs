namespace ShopUI
{
    public enum MenuType {
        MainMenu,
        Exit,
        AddCustomer,
        SearchCustomer,
    }
    public interface IMenu{

        
        /// <summary>
        /// Will dispaly the menu and user vhoices in the terminal
        /// </summary>
        void Display();
        /// <summary>
        /// Will record the user's choice and change/route yoru menu based on that choice
        /// </summary>
        /// <returns> Return the menu that will change your screen
        string UserChoice();
    }
    
}