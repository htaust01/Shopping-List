using System;
using ShoppingList.Data;

namespace ShoppingList.Logic
{
	public interface IGroceryListLogic
	{
        void AddItemToGroceryList(GroceryItem item);

        GroceryList GetGroceryList();

        int GetIndexToInsertBySectionAisle(GroceryItem item);

        GroceryItem GetMostExpensiveItemInList();
    }
}

