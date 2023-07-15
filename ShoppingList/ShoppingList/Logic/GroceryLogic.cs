using System;
using ShoppingList.Models;
using ShoppingList.Validators;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Logic
{
	public class GroceryLogic : IGroceryLogic
	{
		public List<GroceryItem> _groceryItems;
        public GroceryList _groceryList;

        public GroceryLogic()
		{
			_groceryItems = new List<GroceryItem>();
            _groceryList = new GroceryList();
        }

        public void AddGroceryItem(GroceryItem item)
        {
            var validator = new GroceryItemValidator();
            if(validator.Validate(item as GroceryItem).IsValid)
            {
                _groceryItems.Add(item);
            }
            else
            {
                throw new ValidationException("The item is not a valid grocery item");
            }
        }

        public List<GroceryItem> GetAllGroceryItems()
        {
            return _groceryItems;
        }

        public GroceryItem GetGroceryItemByName(string name)
        {
            foreach(GroceryItem item in _groceryItems)
            {
                if (item.Name == name) return item;
            }
            return null;
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

