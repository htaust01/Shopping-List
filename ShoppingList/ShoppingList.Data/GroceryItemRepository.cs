using System;
using Microsoft.EntityFrameworkCore;

namespace ShoppingList.Data
{
	public class GroceryItemRepository : IGroceryItemRepository
	{
		private readonly GroceryItemContext _dbContext;

		public GroceryItemRepository()
		{
			_dbContext = new GroceryItemContext();
		}

        public void AddGroceryItem(GroceryItem item)
        {
            _dbContext.GroceryItems.Add(item);
            _dbContext.SaveChanges();
        }

        public List<GroceryItem> GetAllGroceryItems()
        {
            return _dbContext.GroceryItems.ToList();
        }

        public GroceryItem GetGroceryItemById(int id)
        {
            return _dbContext.GroceryItems.SingleOrDefault(x => x.GroceryItemId == id);
        }
    }
}