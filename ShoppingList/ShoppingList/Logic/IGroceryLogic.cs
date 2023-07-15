using System;
using ShoppingList.Models;

namespace ShoppingList.Logic
{
	public interface IGroceryLogic
	{
		public void AddGroceryItem(GroceryItem item);

		public List<GroceryItem> GetAllGroceryItems();

		public GroceryItem GetGroceryItemByName(string name);

        public void AddItemToGroceryList(GroceryItem item);

        public GroceryList GetGroceryList();

        public int GetIndexToInsertBySectionAisle(GroceryItem item);
    }
}

