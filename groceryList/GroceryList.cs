namespace groceryList;

public class GroceryList
//
// maintain a list (ShoppingList) of GroceryItem objects.
// implement helper methods to remove some detail from the user interface.
//
{
    private List<GroceryItem> ShoppingList = new();

    public void AddGroceryItem(string ItemName, int Quantity)
    {
        AddGroceryItem(new GroceryItem(ItemName, Quantity));
    }
    
    public void AddGroceryItem(GroceryItem item)
    {
        ShoppingList.Add(item);
    }

    public void MakeItemPurchasedAt(int itemIndex)
    {
        ShoppingList.ElementAt(itemIndex).MakePurchased();
    }

    public void RemoveGroceryItemAt(int itemIndex)
    {
        ShoppingList.RemoveAt(itemIndex);
    }

    public void DisplayList()
    {
        foreach(GroceryItem each in ShoppingList)
        {
            Console.WriteLine(each);
        }
    }

    public void DisplayNumberedList()
    {
        int index = 1;
        foreach(GroceryItem each in ShoppingList)
        {
            Console.WriteLine($"(Item {index++})  {each}");
        }             
    }

    public bool ListIsEmpty()
    {
        return NumberOfItems() == 0;
    }

    public int NumberOfItems()
    {
        return ShoppingList.Count();
    }
}