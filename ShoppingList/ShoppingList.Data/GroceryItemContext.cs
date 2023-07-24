using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            options.UseSqlite($"Data Source=../../../../ShoppingList.Data/grocery.db");
            //options.UseSqlite($"Data Source=grocery.db");

            //string path = Environment.CurrentDirectory.ToString() + "/grocery.db";

            //if (!File.Exists(path))
            //    options.UseSqlite($"Data Source=grocery.db");
            //else
            //    options.UseSqlite($"Data Source=../../../../ShoppingList.Data/grocery.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroceryItem>().ToTable("GroceryItems");
        }
    }
}