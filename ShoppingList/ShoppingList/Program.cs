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
                Console.Write("What is the name of the grocery item you would like to view? ");
                var groceryItemName = Console.ReadLine();
                Console.WriteLine();
                var groceryItem = groceryItemLogic.GetGroceryItemByName(groceryItemName);
                Console.WriteLine(JsonSerializer.Serialize(groceryItem));
                Console.WriteLine();
            }
            if (userInput == "3")
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

            userInput = DisplayMenuAndGetInput();
        }
    }

    static string DisplayMenuAndGetInput()
    {
        Console.WriteLine("Press 1 to add a grocery item");
        Console.WriteLine("Press 2 to view a grocery item");
        Console.WriteLine("Press 3 to view all grocery items");
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