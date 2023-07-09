using System;
namespace ShoppingList.Models
{
	public class GroceryItem
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? Section { get; set; }

		public int Aisle { get; set; }

		public decimal Price { get; set; }
	}
}

