using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingList.Logic;
using ShoppingList.Models;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServiceCollection();

        var groceryItemLogic = services.GetService<IGroceryItemLogic>();

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "PB",
                Section = "Grocery",
                Aisle = 8,
                Price = 6.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Jelly",
                Section = "Grocery",
                Aisle = 9,
                Price = 6.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Chips",
                Section = "Grocery",
                Aisle = 5,
                Price = 4.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Milk",
                Section = "Dairy",
                Aisle = 1,
                Price = 1.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Cheese",
                Section = "Dairy",
                Aisle = 2,
                Price = 2.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Bread",
                Section = "Frozen",
                Aisle = 2,
                Price = 7.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Ice Cream",
                Section = "Frozen",
                Aisle = 1,
                Price = 9.95m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Apple",
                Section = "Produce",
                Aisle = 1,
                Price = 0.75m
            });

        groceryItemLogic.AddGroceryItem(
            new GroceryItem
            {
                Name = "Banana",
                Section = "Produce",
                Aisle = 2,
                Price = 0.65m
            });



        GroceryList groceryList = new GroceryList();

        string userInput = DisplayMenuAndGetInput();

        while (userInput.ToLower() != "exit")
        {
            if (userInput == "1")
            {
                var groceryItem = new GroceryItem();
                Console.WriteLine("Creating a grocery item...");
                Console.WriteLine();
                Console.Write("Enter the name of the grocery item: ");
                groceryItem.Name = Console.ReadLine();
                Console.Write("Enter the section: ");
                groceryItem.Section = Console.ReadLine();
                Console.Write("Enter the aisle: ");
                groceryItem.Aisle = int.Parse(Console.ReadLine());
                Console.Write("Enter the price: ");
                groceryItem.Price = decimal.Parse(Console.ReadLine());
                groceryItemLogic.AddGroceryItem(groceryItem);
                Console.WriteLine();
                Console.WriteLine($"Added {groceryItem.Name} to grocery items");
                Console.WriteLine();
            }
            if (userInput == "2")
            {
                Console.WriteLine("Creating a grocery item...");
                Console.WriteLine();
                Console.WriteLine("Enter the grocery item in JSON format:");
                var groceryItemAsJSON = Console.ReadLine();
                var groceryItem = JsonSerializer.Deserialize<GroceryItem>(groceryItemAsJSON);
                groceryItemLogic.AddGroceryItem(groceryItem);
                Console.WriteLine();
                Console.WriteLine($"Added {groceryItem.Name} to grocery items");
                Console.WriteLine();
            }
            if (userInput == "3")
            {
                Console.Write("What is the name of the grocery item you would like to view? ");
                var groceryItemName = Console.ReadLine();
                Console.WriteLine();
                var groceryItem = groceryItemLogic.GetGroceryItemByName(groceryItemName);
                Console.WriteLine(JsonSerializer.Serialize(groceryItem));
                Console.WriteLine();
                // Add to GroceryList
                Console.Write("Would you like to add this item to your grocery list(y/n)? ");
                if(Console.ReadLine().ToLower() == "y")
                {
                    groceryList.AddGroceryItemToGroceryList(groceryItem);
                    Console.WriteLine($"{groceryItem.Name} added to your grocery list.");
                }
            }
            if (userInput == "4")
            {
                Console.WriteLine("The store has the following grocery items: ");
                Console.WriteLine();
                var allItems = groceryItemLogic.GetAllGroceryItems();
                foreach (var item in allItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
            }
            if (userInput == "5")
            {
                Console.WriteLine("Grocery List: ");
                foreach (var item in groceryList.GroceryItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
                Console.WriteLine($"Total Price: {groceryList.TotalPrice}");
                Console.WriteLine();
            }
            userInput = DisplayMenuAndGetInput();
        }
    }

    static string DisplayMenuAndGetInput()
    {
        Console.WriteLine("Press 1 to add a grocery item");
        Console.WriteLine("Press 2 to add a grocery item as JSON");
        Console.WriteLine("Press 3 to view a grocery item");
        Console.WriteLine("Press 4 to view all grocery items");
        Console.WriteLine("Press 5 to view your grocery list");
        Console.WriteLine("Type 'exit' to quit");
        string userInput = Console.ReadLine();
        Console.WriteLine();
        return userInput;
    }

    static IServiceProvider CreateServiceCollection()
    {
        return new ServiceCollection()
            .AddTransient<IGroceryItemLogic, GroceryItemLogic>()
            .BuildServiceProvider();
    }
}


// {"Name": "PB", "Section": "Grocery", "Aisle": 8, "Price": 6.95}
// {"Name": "Jelly", "Section": "Grocery", "Aisle": 9, "Price": 5.95}
// {"Name": "Potato Chips", "Section": "Grocery", "Aisle": 5, "Price": 4.95}
// {"Name": "Milk", "Section": "Dairy", "Aisle": 1, "Price": 1.95}
// {"Name": "Cheese", "Section": "Dairy", "Aisle": 2, "Price": 2.95}
// {"Name": "Ezekiel Bread", "Section": "Frozen", "Aisle": 2, "Price": 7.95}
// {"Name": "Butter Pecan Ice Cream", "Section": "Frozen", "Aisle": 1, "Price": 9.95}
// {"Name": "Apples", "Section": "Produce", "Aisle": 1, "Price": 4.95}
// {"Name": "Bananas", "Section": "Produce", "Aisle": 2, "Price": 0.95}