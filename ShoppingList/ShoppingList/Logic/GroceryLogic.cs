using System;
using ShoppingList.Validators;
using ShoppingList.Data;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Logic
{
	public class GroceryLogic : IGroceryLogic
	{
		private readonly IGroceryItemRepository _groceryItemRepo;
        public GroceryList _groceryList;

        public GroceryLogic(IGroceryItemRepository groceryItemRepo)
		{
            _groceryItemRepo = groceryItemRepo;
            _groceryList = new GroceryList();
        }

        public void AddGroceryItem(GroceryItem item)
        {
            var validator = new GroceryItemValidator();
            if(validator.Validate(item).IsValid)
            {
                _groceryItemRepo.AddGroceryItem(item);
            }
            else
            {
                throw new ValidationException("The item is not a valid grocery item");
            }
        }

        public List<GroceryItem> GetAllGroceryItems()
        {
            return _groceryItemRepo.GetAllGroceryItems();
        }

        public GroceryItem GetGroceryItemById(int id)
        {
            return _groceryItemRepo.GetGroceryItemById(id);
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

        public List<GroceryItem> GetGroceryItemsByName(string name)
        {
            var namedItems = _groceryItemRepo.GetGroceryItemsByName(name);
            return namedItems;
        }

        public List<GroceryItem> GetGroceryItemsBySection(string section)
        {
            var sectionItems = _groceryItemRepo.GetGroceryItemsBySection(section);
            return sectionItems;
        }

        public GroceryItem GetMostExpensiveItemInList()
        {
            if (_groceryList.GroceryItems.Count == 0) return null;
            GroceryItem expensiveItem = _groceryList.GroceryItems[0];
            foreach(var item in _groceryList.GroceryItems)
            {
                if (item.Price > expensiveItem.Price) expensiveItem = item;
            }
            return expensiveItem;
        }
    }
}