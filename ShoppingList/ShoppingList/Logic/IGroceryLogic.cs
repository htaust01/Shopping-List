using System;
using ShoppingList.Data;

namespace ShoppingList.Logic
{
	public interface IGroceryLogic
	{
		public void AddGroceryItem(GroceryItem item);

		public List<GroceryItem> GetAllGroceryItems();

		public GroceryItem GetGroceryItemById(int id);

        public void AddItemToGroceryList(GroceryItem item);

        public GroceryList GetGroceryList();

        public int GetIndexToInsertBySectionAisle(GroceryItem item);
    }
}