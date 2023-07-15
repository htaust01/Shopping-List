using System;
namespace ShoppingList.Models
{
	public class GroceryList
	{
		public List<GroceryItem> GroceryItems { get; set; }

		public decimal TotalPrice { get; set; }

		public GroceryList()
		{
			TotalPrice = 0m;
			GroceryItems = new List<GroceryItem>();
		}

		public int FindIndexToInsert(GroceryItem item)
		{
			if (GroceryItems.Count == 0) return 0;
			List<string> sections = new List<string> { "Produce", "Grocery", "Dairy", "Frozen" };
			for(int index = 0; index < GroceryItems.Count; index++)
			{
				if (sections.IndexOf(item.Section) > sections.IndexOf(GroceryItems[index].Section)) continue;
				if (sections.IndexOf(item.Section) == sections.IndexOf(GroceryItems[index].Section)
                    && item.Aisle > GroceryItems[index].Aisle) continue;
                return index;
			}
			return GroceryItems.Count;
		}

		public void AddGroceryItemToGroceryList(GroceryItem item)
		{
			int index = FindIndexToInsert(item);
			GroceryItems.Insert(index, item);
			TotalPrice += item.Price;
		}
	}
}

