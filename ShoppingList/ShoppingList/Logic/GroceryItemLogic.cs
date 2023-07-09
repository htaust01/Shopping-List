using System;
using ShoppingList.Models;

namespace ShoppingList.Logic
{
	public class GroceryItemLogic : IGroceryItemLogic
	{
		public List<GroceryItem> _items;

		public GroceryItemLogic()
		{
			_items = new List<GroceryItem>();
		}

        public void AddGroceryItem(GroceryItem item)
        {
            _items.Add(item);
        }

        public List<GroceryItem> GetAllGroceryItems()
        {
            return _items;
        }

        public GroceryItem GetGroceryItemByName(string name)
        {
            foreach(GroceryItem item in _items)
            {
                if (item.Name == name) return item;
            }
            return null;
        }
    }
}

