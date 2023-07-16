using System;

namespace ShoppingList.Data
{
	public class GroceryItem
	{
		public int GroceryItemId { get; set; }

		public string Name { get; set; }

		public string Section { get; set; }

		public int Aisle { get; set; }

		public decimal Price { get; set; }
	}
}