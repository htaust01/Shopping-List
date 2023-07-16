using System;
using FluentValidation;
using ShoppingList.Data;

namespace ShoppingList.Validators
{
	public class GroceryItemValidator : AbstractValidator<GroceryItem>
	{
		public GroceryItemValidator()
		{
			var validSections = new List<string>() { "Produce", "Grocery", "Dairy", "Frozen" };
			RuleFor(groceryItem => groceryItem.Section)
				.Must(x => validSections.Contains(x))
				.WithMessage("Please only use: " + String.Join(",", validSections));
			RuleFor(groceryItem => groceryItem.Name).NotEmpty();
			RuleFor(groceryItem => groceryItem.Price).GreaterThanOrEqualTo(0);
            RuleFor(groceryItem => groceryItem.Aisle).GreaterThanOrEqualTo(1);
        }
	}
}