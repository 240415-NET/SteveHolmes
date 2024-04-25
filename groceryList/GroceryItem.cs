namespace groceryList;

public class GroceryItem
//
// represents an item in a grocery list.
//
{
    // private attributes - nobody else needs to get to these.
    private string Name {get; set;}
    private int Quantity {get; set;}
    private bool WasPurchased {get; set;}

    //Constructors
    private GroceryItem() {}

    public GroceryItem(string Name, int Quantity)
    {
        this.Name = Name;
        this.Quantity = Quantity;
        this.WasPurchased = false;
    }

    // Instance methods
    public void MakePurchased()
    {
        WasPurchased = true;
    }
   
    public override string ToString()
    {
        string PurchasedText = "";
        if (WasPurchased)
        {
            PurchasedText = "- WAS PURCHASED";
        }
        return $"{Quantity} x {Name}  {PurchasedText}";
    }
}