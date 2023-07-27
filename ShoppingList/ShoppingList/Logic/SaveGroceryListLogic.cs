using System;
using System.Text.Json;
using ShoppingList.Data;

namespace ShoppingList.Logic
{
	static public class SaveGroceryListLogic
	{
        static public async Task SaveGroceryListToFile(GroceryList groceryList)
        {
            List<string> groceryListAsStrings = new List<string>();
            groceryListAsStrings.Add("Section - Aisle  - Name - Price\n");
            foreach (var item in groceryList.GroceryItems)
            {
                string itemString = $"{item.Section} - {item.Aisle} - {item.Name} - {item.Price}";
                groceryListAsStrings.Add(itemString);
            }
            groceryListAsStrings.Add($"\nTotal Price - {groceryList.TotalPrice}");
            string textToSave = String.Join("\n", groceryListAsStrings);
            string fileName = "grocerylist.txt";
            await File.WriteAllTextAsync(fileName, textToSave);
        }
    }
}

