using System;
using FluentValidation;
using ShoppingList.Models;

namespace ShoppingList.Validators
{
	public class GroceryItemValidator : AbstractValidator<GroceryItem>
	{
		public GroceryItemValidator()
		{
			var validSections = new List<string>() { "Produce", "Grocery", "Dairy", "Frozen" };
			RuleFor(groceryItem => groceryItem.Section)
				.Must(x => validSections.Contains(x))
				.WithMessage("Please only use: " + String.Join("'", validSections));
			RuleFor(groceryItem => groceryItem.Name).NotEmpty();
			RuleFor(groceryItem => groceryItem.Price).GreaterThanOrEqualTo(0);
			// Rule for Aisle
		}
	}
}

