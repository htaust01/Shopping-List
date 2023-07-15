using System;
using ShoppingList.Models;

namespace ShoppingList.Logic
{
	public class GroceryListLogic : IGroceryListLogic
	{
        public GroceryList _groceryList;

		public GroceryListLogic()
		{
            _groceryList = new GroceryList();
		}

        public void AddItemToGroceryList(GroceryItem item)
        {
            int index = GetIndexToInsertBySectionAisle(item);
            _groceryList.GroceryItems.Insert(index, item);
            _groceryList.TotalPrice += item.Price;
        }

        public GroceryList GetGroceryList()
        {
            return _groceryList;
        }

        public int GetIndexToInsertBySectionAisle(GroceryItem item)
        {
            if (_groceryList.GroceryItems.Count == 0) return 0;
            List<string> sections = new List<string> { "Produce", "Grocery", "Dairy", "Frozen" };
            for (int index = 0; index < _groceryList.GroceryItems.Count; index++)
            {
                if (sections.IndexOf(item.Section) > sections.IndexOf(_groceryList.GroceryItems[index].Section)) continue;
                if (sections.IndexOf(item.Section) == sections.IndexOf(_groceryList.GroceryItems[index].Section)
                    && item.Aisle > _groceryList.GroceryItems[index].Aisle) continue;
                return index;
            }
            return _groceryList.GroceryItems.Count;
        }
    }
}

