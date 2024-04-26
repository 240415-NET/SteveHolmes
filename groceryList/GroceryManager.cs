namespace groceryList;

public class GroceryManager
//
// GroceryManager
// To handle user interaction.
// Use ShoppingList (instance of GroceryList) to get things done.
//
{
    GroceryList ShoppingList = new GroceryList();


    public void Start()
    //
    // main control loop - present menu, obtain/validate input, perform selected action.
    //
    {
        string UserSelection;    
        do 
        {
            Console.Clear();
            Console.WriteLine("\nMANAGE SHOPPING LIST");
            Console.WriteLine("--------------------");
            Console.WriteLine("\n 1  Display shopping list.");
            Console.WriteLine(" 2  Add item to shopping list.");
            Console.WriteLine(" 3  Mark shopping list item as purchased.");
            Console.WriteLine(" 4  Remove item from the shopping list.");
            Console.WriteLine(" Q  Quit/Exit.");
            Console.Write("\nEnter selection: ");

            UserSelection = Console.ReadLine().ToLower();

            switch (UserSelection)
            {
                case "1":
                    DisplayShoppingList();
                    break;
                case "2":
                    AddItemToShoppingList();
                    break;
                case "3":
                    MarkItemAsPurchased();
                    break;
                case "4": 
                    RemoveItemFromShoppingList();
                    break;
                case "q":
                    break;
                default:
                    Console.Write("\nSelection not recognized.");
                    HitAnyKeyToContinue();
                    break;
            }
        }
        while (UserSelection != "q");
    }  

    void DisplayShoppingList()
    // if the ShoppingList is not empty, we ask it to display its list of items.
    {
        Console.Clear();
        Console.WriteLine("\nSHOPPING LIST");
        Console.WriteLine("-------------\n");
        if (ShoppingList.ListIsEmpty())
        {
            Console.WriteLine("The shopping list is empty.");
        }
        else
        {
            ShoppingList.DisplayList();  
        }
        Console.WriteLine();
        HitAnyKeyToContinue();
    } 

    void AddItemToShoppingList()
    //  prompt for item and quantity.
    //  ask the ShoppingList to add a new GroceryItem created from the user input.
    //  display the updated list of entries.
    {
        Console.Clear();
        Console.WriteLine("\nADD ITEM TO SHOPPING LIST");
        Console.WriteLine("-------------------------\n");
        Console.Write("What do you want to buy? ");
        string ItemName = Console.ReadLine();
        Console.Write("How many? ");
        int Quantity = NumberFromUser();

        ShoppingList.AddGroceryItem(ItemName, Quantity);
        DisplayShoppingList();
    }   

    void MarkItemAsPurchased()
    //  prompt for which item to mark as purchased.
    //  ask the ShoppingList to mark the item as purchased.
    //  display the updated list of entries.
    {
        Console.Clear();
        Console.WriteLine("\nMARK SHOPPING LIST ITEM AS PURCHASED");
        Console.WriteLine("------------------------------------\n");
        if (ShoppingList.ListIsEmpty())
            {
                Console.WriteLine("The shopping list is empty.\n");
                HitAnyKeyToContinue();
                return;    
            }
        ShoppingList.DisplayNumberedList();
        Console.Write("\nEnter the number of the item to be marked as purchased: ");
        int ItemIndex = ItemNumberFromUser() - 1;

        ShoppingList.MakeItemPurchasedAt(ItemIndex);
        DisplayShoppingList();
    }  
    
    void RemoveItemFromShoppingList()
    // prompt for which item to remove from the list.
    // ask the ShoppingList to remove the item.
    // display the updated list of entries.
    {
        Console.Clear();
        Console.WriteLine("\nREMOVE ITEM FROM SHOPPING LIST");
        Console.WriteLine("------------------------------\n");
        if (ShoppingList.ListIsEmpty())
            {
                Console.WriteLine("The shopping list is empty.\n");
                HitAnyKeyToContinue();
                return;    
            }
        ShoppingList.DisplayNumberedList();
        Console.Write("\nEnter the number of the item to be removed: ");
        int ItemIndex = ItemNumberFromUser() - 1;

        ShoppingList.RemoveGroceryItemAt(ItemIndex);
        DisplayShoppingList();
    }  

    int NumberFromUser()
    //  obtain a number from the user. 
    {
        bool UserInputIsValid = true;
        int UserNumber = 0;
          
        do 
        {  
            if (UserInputIsValid == false)
            {
                Console.Write("Invalid Entry. Please enter a number: ");
            } 
            string UserString = Console.ReadLine();
            UserInputIsValid = true;
            try 
                {
                    UserNumber = Convert.ToInt32(UserString); 
                }
            catch (Exception e)
                {
                    UserInputIsValid = false; 
                }
        }
        while (UserInputIsValid != true);
        return UserNumber;
    }   

    int ItemNumberFromUser()
    //  obtain a number from the user.
    //  ensure that the number corresponds to an shopping list entry.
    {
        int UserInput;
        bool NumberIsInRange = true;
        do {
            if (!NumberIsInRange)
            {
                Console.Write("Item number not recognised. Enter the item number: ");
            }
            UserInput = NumberFromUser();
            NumberIsInRange = UserInput > 0 && UserInput <= ShoppingList.NumberOfItems();
        }
        while (!NumberIsInRange);
        return UserInput;
    } 

    void HitAnyKeyToContinue()
    {
        Console.Write("\nHit any key to continue...");
        Console.ReadKey();
    } 
}