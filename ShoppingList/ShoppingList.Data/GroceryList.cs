using System;
namespace ShoppingList.Data
{
	public class GroceryList
	{
		public List<GroceryItem> GroceryItems { get; set; }

		public decimal TotalPrice { get; set; }

		public GroceryList()
		{
			TotalPrice = 0.00m;
			GroceryItems = new List<GroceryItem>();
		}
	}
}