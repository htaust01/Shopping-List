using System;
namespace ShoppingList.Models
{
	public class GroceryList
	{
		public List<GroceryItem> GroceryItems { get; set; }

		public decimal TotalPrice { get; set; }

		public GroceryList()
		{
		}
	}
}

