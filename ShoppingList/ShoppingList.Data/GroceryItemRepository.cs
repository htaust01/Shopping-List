using System;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

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

        public void RemoveGroceryItem(GroceryItem item)
        {
            _dbContext.GroceryItems.Remove(item);
            _dbContext.SaveChanges();
        }

        public void UpdateGroceryItem(GroceryItem item)
        {
            _dbContext.GroceryItems.Update(item);
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

        public List<GroceryItem> GetGroceryItemsByName(string name)
        {
            return _dbContext.GroceryItems
                .FromSql($"SELECT * FROM [GroceryItems] WHERE Name = {name}")
                .ToList();
        }

        public List<GroceryItem> GetGroceryItemsBySection(string section)
        {
            return _dbContext.GroceryItems
                .FromSql($"SELECT * FROM [GroceryItems] WHERE Section = {section}")
                .ToList();
        }

        public void SeedGroceryItems()
        {
            if(!_dbContext.GroceryItems.Any())
            {
                _dbContext.GroceryItems.AddRange(
                    new GroceryItem
                    {
                        Name = "Peanut Butter",
                        Section = "Grocery",
                        Aisle = 7,
                        Price = 6.01m
                    },
                    new GroceryItem
                    {
                        Name = "Jelly",
                        Section = "Grocery",
                        Aisle = 8,
                        Price = 5.01m
                    },
                    new GroceryItem
                    {
                        Name = "Potato Chips",
                        Section = "Grocery",
                        Aisle = 5,
                        Price = 4.01m
                    },
                    new GroceryItem
                    {
                        Name = "Apples",
                        Section = "Produce",
                        Aisle = 3,
                        Price = 5.01m
                    },
                    new GroceryItem
                    {
                        Name = "Bananas",
                        Section = "Produce",
                        Aisle = 4,
                        Price = 3.01m
                    },
                    new GroceryItem
                    {
                        Name = "Cuties",
                        Section = "Produce",
                        Aisle = 2,
                        Price = 4.01m
                    },
                    new GroceryItem
                    {
                        Name = "Butter",
                        Section = "Dairy",
                        Aisle = 2,
                        Price = 5.01m
                    },
                    new GroceryItem
                    {
                        Name = "Milk",
                        Section = "Dairy",
                        Aisle = 1,
                        Price = 2.01m
                    },
                    new GroceryItem
                    {
                        Name = "Cheese",
                        Section = "Dairy",
                        Aisle = 3,
                        Price = 3.01m
                    },
                    new GroceryItem
                    {
                        Name = "Ezekiel Bread",
                        Section = "Frozen",
                        Aisle = 3,
                        Price = 7.01m
                    },
                    new GroceryItem
                    {
                        Name = "Ice Cream Sandwiches",
                        Section = "Frozen",
                        Aisle = 2,
                        Price = 5.01m
                    },
                    new GroceryItem
                    {
                        Name = "Waffles",
                        Section = "Frozen",
                        Aisle = 1,
                        Price = 4.01m
                    }
                    );
                _dbContext.SaveChanges();
            }
        }
    }
}