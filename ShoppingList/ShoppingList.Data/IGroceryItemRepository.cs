using System;

namespace ShoppingList.Data
{
	public interface IGroceryItemRepository
	{
		void AddGroceryItem(GroceryItem item);

        void RemoveGroceryItem(GroceryItem item);

		void UpdateGroceryItem(GroceryItem item);

        GroceryItem GetGroceryItemById(int id);

		List<GroceryItem> GetAllGroceryItems();

		List<GroceryItem> GetGroceryItemsByName(string name);

        List<GroceryItem> GetGroceryItemsBySection(string section);

        void SeedGroceryItems();
    }
}