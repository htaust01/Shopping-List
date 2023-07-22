using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingList.Logic;
using ShoppingList.Data;
using System.Text.Json;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServiceCollection();

        var groceryLogic = services.GetService<IGroceryLogic>();

        string emailAddress = GetEmailAddress();

        while (true)
        {
            if (IsValidEmailAddress(emailAddress)) break;
            else
            {
                Console.WriteLine("The email address you entered is not a valid email address.");
                Console.WriteLine();
                emailAddress = GetEmailAddress();
            }
        }
        Console.WriteLine($"Your email address is : {emailAddress}");
        Console.WriteLine();

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
                groceryLogic.AddGroceryItem(groceryItem);
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
                groceryLogic.AddGroceryItem(groceryItem);
                Console.WriteLine();
                Console.WriteLine($"Added {groceryItem.Name} to grocery items");
                Console.WriteLine();
            }
            if (userInput == "3")
            {
                Console.Write("What is the id of the grocery item you would like to view? ");
                var groceryItemId = int.Parse(Console.ReadLine());
                Console.WriteLine();
                var groceryItem = groceryLogic.GetGroceryItemById(groceryItemId);
                Console.WriteLine(JsonSerializer.Serialize(groceryItem));
                Console.WriteLine();
                // Add to GroceryList
                Console.Write("Would you like to add this item to your grocery list(y/n)? ");
                if(Console.ReadLine().ToLower() == "y")
                {
                    groceryLogic.AddItemToGroceryList(groceryItem);
                    Console.WriteLine($"{groceryItem.Name} added to your grocery list.");
                }
            }
            if (userInput == "4")
            {
                Console.Write("What is the name of the grocery item you would like to view? ");
                var groceryItemName = Console.ReadLine();
                Console.WriteLine();
                var namedItems = groceryLogic.GetGroceryItemsByName(groceryItemName);
                foreach (var item in namedItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
            }
            if (userInput == "5")
            {
                Console.Write("What is the section of the grocery items you would like to view(Produce, Grocery, Dairy, Frozen)? ");
                var groceryItemSection = Console.ReadLine();
                Console.WriteLine();
                var sectionItems = groceryLogic.GetGroceryItemsBySection(groceryItemSection);
                foreach (var item in sectionItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
            }
            if (userInput == "6")
            {
                Console.WriteLine("The store has the following grocery items: ");
                Console.WriteLine();
                var allItems = groceryLogic.GetAllGroceryItems();
                foreach (var item in allItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
            }
            if (userInput == "7")
            {
                Console.WriteLine("Grocery List: ");
                var groceryList = groceryLogic.GetGroceryList();
                foreach (var item in groceryList.GroceryItems)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item));
                }
                Console.WriteLine();
                Console.WriteLine($"Total Price: {groceryList.TotalPrice}");
                Console.WriteLine();
            }
            if(userInput == "8")
            {
                Console.WriteLine("The most expensive item in your grocery list is:");
                Console.WriteLine();
                var mostExpensiveItem = groceryLogic.GetMostExpensiveItemInList();
                Console.WriteLine(JsonSerializer.Serialize(mostExpensiveItem));
                Console.WriteLine();
            }
            userInput = DisplayMenuAndGetInput();
        }
    }

    private static bool IsValidEmailAddress(string emailAddress)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return regex.IsMatch(emailAddress);
    }

    static string GetEmailAddress()
    {
        Console.Write("Enter your email address: ");
        string emailAddress = Console.ReadLine();
        Console.WriteLine();
        return emailAddress;
    }

    static string DisplayMenuAndGetInput()
    {
        Console.WriteLine("Press 1 to add a grocery item");
        Console.WriteLine("Press 2 to add a grocery item as JSON");
        Console.WriteLine("Press 3 to view a grocery item by Id");
        Console.WriteLine("Press 4 to view a grocery item by name");
        Console.WriteLine("Press 5 to view a grocery items by section");
        Console.WriteLine("Press 6 to view all grocery items");
        Console.WriteLine("Press 7 to view your grocery list");
        Console.WriteLine("Press 8 to view the most expensive item in your grocery list");
        Console.WriteLine("Type 'exit' to quit");
        string userInput = Console.ReadLine();
        Console.WriteLine();
        return userInput;
    }

    static IServiceProvider CreateServiceCollection()
    {
        return new ServiceCollection()
            .AddTransient<IGroceryLogic, GroceryLogic>()
            .AddTransient<IGroceryItemRepository, GroceryItemRepository>()
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
// {"Name": "Milk", "Section": "Dairy", "Aisle": 1, "Price": 10.95}

// Current Features:
// Query Database using raw SQL query (Get grocery item by name and also by section)
// Create List, populate it with several values, retrieve at least one value, and use it in your program (Grocery List Most Expensive Item)
// Implement a regex to ensure an email address is always stored and displayed in the same format

// Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");