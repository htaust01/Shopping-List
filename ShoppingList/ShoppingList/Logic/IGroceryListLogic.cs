using System;
using ShoppingList.Models;

namespace ShoppingList.Logic
{
	public interface IGroceryListLogic
	{
		public void AddItemToGroceryList(GroceryItem item);

		public GroceryList GetGroceryList();

		public int GetIndexToInsertBySectionAisle(GroceryItem item);
	}
}

