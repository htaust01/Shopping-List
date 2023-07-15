using System;
using ShoppingList.Models;
using ShoppingList.Validators;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Logic
{
	public class GroceryItemLogic : IGroceryItemLogic
	{
		public List<GroceryItem> _groceryItems;

		public GroceryItemLogic()
		{
			_groceryItems = new List<GroceryItem>();
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
    }
}

