using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ShoppingList.Data
{
	public class GroceryItemContext : DbContext
	{
		public DbSet<GroceryItem> GroceryItems { get; set; }

        public string DbPath { get; }

        public GroceryItemContext()
		{
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "grocery.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}