using System;
using ShoppingList.Data;

namespace ShoppingList.Logic
{
	public interface IGroceryItemLogic
	{
        void AddGroceryItem(GroceryItem item);

        List<GroceryItem> GetAllGroceryItems();

        GroceryItem GetGroceryItemById(int id);

        List<GroceryItem> GetGroceryItemsByName(string name);

        List<GroceryItem> GetGroceryItemsBySection(string section);
    }
}

