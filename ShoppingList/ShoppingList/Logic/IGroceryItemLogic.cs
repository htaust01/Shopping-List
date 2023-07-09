using System;
using ShoppingList.Models;

namespace ShoppingList.Logic
{
	public interface IGroceryItemLogic
	{
		public void AddGroceryItem(GroceryItem item);

		public List<GroceryItem> GetAllGroceryItems();

		public GroceryItem GetGroceryItemByName(string name);
	}
}

