using System;

namespace ShoppingList.Data
{
	public interface IGroceryItemRepository
	{
		void AddGroceryItem(GroceryItem item);

		GroceryItem GetGroceryItemById(int id);

		List<GroceryItem> GetAllGroceryItems();
	}
}