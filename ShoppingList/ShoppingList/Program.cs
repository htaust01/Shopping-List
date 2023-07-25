using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingList.Logic;
using ShoppingList.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using ShoppingList.UI;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServiceCollection();

        var groceryLogic = services.GetService<IGroceryLogic>();

        UserInterface.WelcomeBanner();

        UserInterface.MainMenu(groceryLogic);
    }

    static IServiceProvider CreateServiceCollection()
    {
        return new ServiceCollection()
            .AddTransient<IGroceryLogic, GroceryLogic>()
            .AddTransient<IGroceryItemRepository, GroceryItemRepository>()
            .BuildServiceProvider();
    }
    
}

// Current Features:
// Query Database using raw SQL query (Get grocery item by name and also by section)
// Create List, populate it with several values, retrieve at least one value, and use it in your program (Grocery List Most Expensive Item)
// Implement a regex to ensure an email address is always stored and displayed in the same format

// Fix Price so it isn't text using the next two lines in GroceryItem model
// using System.ComponentModel.DataAnnotations.Schema;
// [Column(TypeName = "decimal(18, 2)")]

// Add way to save grocery list to a text file

// Fix database so it is not in special folder

// GroceryList remove logic needs to be fixed, returns false if item not found in list so add message if not found

// Add comments about 2 SOLID principles S and I

// Add at least 3 Unit Tests

// Add Readme and update project plan