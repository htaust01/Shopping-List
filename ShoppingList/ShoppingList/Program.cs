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

        WelcomeBanner();

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

        MainMenu(groceryLogic);

        /*
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
        */
    }

    static string GetEmailAddress()
    {
        Console.Write("Enter your email address: ");
        string emailAddress = Console.ReadLine();
        Console.WriteLine();
        return emailAddress;
    }

    static bool IsValidEmailAddress(string emailAddress)
    {
        // Regex pattern for email
        // First match one or more a-z, A-Z, 0-9, . or - followed by an @
        // followed by one or more a-z, A-Z, 0-9 or - followed by a .
        // followed by 2-3 characters from a-z, A-Z, or 0-9 one or more times
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return regex.IsMatch(emailAddress);
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

    static void WelcomeBanner()
    {
        Console.WriteLine("+-----------------------+");
        Console.WriteLine("|                       |");
        Console.WriteLine("|     Shopping List     |");
        Console.WriteLine("|                       |");
        Console.WriteLine("+-----------------------+");
        Console.WriteLine();
    }

    static string GetUserInput()
    {
        Console.Write("-> ");
        string userInput = Console.ReadLine();
        Console.WriteLine();
        return userInput;
    }

    static void MainMenu(IGroceryLogic groceryLogic)
    {
        bool exitCondition = false;
        while(!exitCondition)
        {
            DisplayMainMenu();
            string choice = GetUserInput();
            switch (choice)
            {
                case "1":
                    GroceryItemMenu(groceryLogic);
                    break;
                case "2":
                    GroceryListMenu(groceryLogic);
                    break;
                case "exit":
                    exitCondition = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("Press 1 to View, Add, Remove, or Edit the Grocery Items");
        Console.WriteLine("Press 2 to View, Add to, or Remove from your Grocery List");
        Console.WriteLine("Type 'exit' to quit");
    }

    static void GroceryItemMenu(IGroceryLogic groceryLogic)
    {
        bool exitCondition = false;
        while (!exitCondition)
        {
            DisplayGroceryItemMenu();
            string choice = GetUserInput();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("The store has the following grocery items: ");
                    Console.WriteLine();
                    var allItems = groceryLogic.GetAllGroceryItems();
                    foreach (var item in allItems)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(item));
                    }
                    Console.WriteLine();
                    break;
                case "2":
                    Console.WriteLine("Enter the grocery item in JSON format:");
                    var groceryItemAsJSON = Console.ReadLine();
                    var groceryItem = JsonSerializer.Deserialize<GroceryItem>(groceryItemAsJSON);
                    groceryLogic.AddGroceryItem(groceryItem);
                    Console.WriteLine();
                    Console.WriteLine($"Added {groceryItem.Name} to grocery items");
                    Console.WriteLine();
                    break;
                case "3":
                    Console.WriteLine("Not yet implemented");
                    break;
                case "4":
                    Console.WriteLine("Not yet implemented");
                    break;
                case "back":
                    exitCondition = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    static void DisplayGroceryItemMenu()
    {
        Console.WriteLine("Press 1 to View the Grocery Items");
        Console.WriteLine("Press 2 to Add a Grocery Item as JSON");
        Console.WriteLine("Press 3 to Edit a Grocery Item");
        Console.WriteLine("Press 4 to Remove a Grocery Item");
        Console.WriteLine("Type 'back' to return to the Main Menu");
        Console.Write("Choice: ");
    }

    static void GroceryListMenu(IGroceryLogic groceryLogic)
    {
        bool exitCondition = false;
        while (!exitCondition)
        {
            DisplayGroceryListMenu();
            string choice = GetUserInput();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Grocery List: ");
                    var groceryList = groceryLogic.GetGroceryList();
                    foreach (var item in groceryList.GroceryItems)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(item));
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Total Price: {groceryList.TotalPrice}");
                    Console.WriteLine();
                    break;
                case "2":
                    Console.WriteLine("What is the name of the grocery item you would like to add? ");
                    var groceryItemToAddName = GetUserInput();
                    var namedItemsToAdd = groceryLogic.GetGroceryItemsByName(groceryItemToAddName);
                    if (namedItemsToAdd.Count == 0)
                        Console.WriteLine("We do not carry that item");
                    else if (namedItemsToAdd.Count == 1)
                        groceryLogic.AddItemToGroceryList(namedItemsToAdd[0]);
                    else
                    {
                        foreach (var item in namedItemsToAdd)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(item));
                        }
                        Console.WriteLine();
                        Console.WriteLine($"Enter the Id of the {groceryItemToAddName} you would like to add.");
                        var groceryItemId = int.Parse(GetUserInput());
                        var namedItemsIds = namedItemsToAdd.Select(x => x.GroceryItemId);
                        if (namedItemsIds.Contains(groceryItemId)) groceryLogic.AddItemToGroceryListById(groceryItemId);
                        else Console.WriteLine($"There is no {groceryItemToAddName} with Id {groceryItemId}");
                    }
                    break;
                case "3":
                    Console.WriteLine("What is the name of the grocery item you would like to remove? ");
                    var groceryItemToRemoveName = GetUserInput();
                    var namedItemsToRemove = groceryLogic.GetGroceryItemsByName(groceryItemToRemoveName);
                    if (namedItemsToRemove.Count == 0)
                        Console.WriteLine("We do not carry that item");
                    else if (namedItemsToRemove.Count == 1)
                        groceryLogic.RemoveGroceryItemFromListById(namedItemsToRemove[0].GroceryItemId);
                    else
                    {
                        foreach (var item in namedItemsToRemove)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(item));
                        }
                        Console.WriteLine();
                        Console.WriteLine($"Enter the Id of the {groceryItemToRemoveName} you would like to remove.");
                        var groceryItemId = int.Parse(GetUserInput());
                        var namedItemsIds = namedItemsToRemove.Select(x => x.GroceryItemId);
                        if (namedItemsIds.Contains(groceryItemId)) groceryLogic.RemoveGroceryItemFromListById(groceryItemId);
                        else Console.WriteLine($"There is no {groceryItemToRemoveName} with Id {groceryItemId}");
                    }
                    break;
                case "4":
                    Console.WriteLine("The most expensive item in your grocery list is:");
                    Console.WriteLine();
                    var mostExpensiveItem = groceryLogic.GetMostExpensiveItemInList();
                    Console.WriteLine(JsonSerializer.Serialize(mostExpensiveItem));
                    Console.WriteLine();
                    break;
                case "back":
                    exitCondition = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    static void DisplayGroceryListMenu()
    {
        Console.WriteLine("Press 1 to View the items in your Grocery List");
        Console.WriteLine("Press 2 to Add an item to your Grocery List");
        Console.WriteLine("Press 3 to Remove an item from your Grocery List");
        Console.WriteLine("Press 4 to View the most expensive item from your Grocery List");
        Console.WriteLine("Type 'back' to return to the Main Menu");
        Console.Write("Choice: ");
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