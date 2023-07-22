using System;

namespace ShoppingList.Data
{
	public interface IGroceryItemRepository
	{
		void AddGroceryItem(GroceryItem item);

		GroceryItem GetGroceryItemById(int id);

		List<GroceryItem> GetAllGroceryItems();

		List<GroceryItem> GetGroceryItemsByName(string name);

        List<GroceryItem> GetGroceryItemsBySection(string section);
    }
}