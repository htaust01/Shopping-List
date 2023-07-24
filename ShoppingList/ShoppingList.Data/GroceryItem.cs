using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Data
{
	public class GroceryItem
	{
		public int GroceryItemId { get; set; }

		public string Name { get; set; }

		public string Section { get; set; }

		public int Aisle { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }
	}
}