using System;
using ShoppingList.Data;

namespace ShoppingList.Logic
{
	public interface IGroceryListLogic
	{
        void AddItemToGroceryList(GroceryItem item);

        void AddItemToGroceryListById(int id);

        bool RemoveGroceryItemFromListById(int id);

        GroceryList GetGroceryList();

        int GetIndexToInsertBySectionAisle(GroceryItem item);

        GroceryItem GetMostExpensiveItemInList();
    }
}

