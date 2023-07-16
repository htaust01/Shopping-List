using System;
using Microsoft.EntityFrameworkCore;

namespace ShoppingList.Data
{
	public class GroceryItemContext : DbContext
	{
		public DbSet<GroceryItem> GroceryItems { get; set; }

		public GroceryItemContext()
		{

		}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(@"Data Source=GroceryItems.db");
        }
    }
}