using System;
using ShoppingList.Data;
using System.Globalization;
using System.Text.Json;
using ShoppingList.Logic;

namespace ShoppingList.UI
{
	static public class UserInterface
	{
        static public void WelcomeBanner()
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

        static public void MainMenu(IGroceryLogic groceryLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplayMainMenu();
                string choice = GetUserInput().ToLower();
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
            Console.WriteLine("Press 1 to View, Add, Remove, or Update the Grocery Items");
            Console.WriteLine("Press 2 to View, Add to, or Remove from your Grocery List");
            Console.WriteLine("Type 'exit' to quit");
        }

        static void GroceryItemMenu(IGroceryLogic groceryLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplayGroceryItemMenu();
                string choice = GetUserInput().ToLower();
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
                        var textinfo = new CultureInfo("en-US", false).TextInfo;
                        Console.WriteLine("Enter the Section.");
                        var section = textinfo.ToTitleCase(GetUserInput().ToLower());
                        var selectedItems = groceryLogic.GetGroceryItemsBySection(section);
                        foreach (var item in selectedItems)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(item));
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Enter the grocery item in JSON format:");
                        var groceryItemAsJSON = GetUserInput();
                        var groceryItem = JsonSerializer.Deserialize<GroceryItem>(groceryItemAsJSON);
                        groceryLogic.AddGroceryItem(groceryItem);
                        Console.WriteLine();
                        Console.WriteLine($"Added {groceryItem.Name} to grocery items.");
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine("What is the name of the grocery item you would like to update?");
                        var groceryItemToUpdateName = GetUserInput();
                        var namedItemsToUpdate = groceryLogic.GetGroceryItemsByName(groceryItemToUpdateName);
                        if (namedItemsToUpdate.Count == 0)
                            Console.WriteLine("We do not carry that item.");
                        else if (namedItemsToUpdate.Count == 1)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(namedItemsToUpdate[0]));
                            Console.WriteLine();
                            Console.Write("Enter the updated Name of the item: ");
                            namedItemsToUpdate[0].Name = Console.ReadLine();
                            Console.Write("Enter the updated Section of the item(Produce, Grocery, Dairy, or Frozen): ");
                            namedItemsToUpdate[0].Section = Console.ReadLine();
                            Console.Write("Enter the updated Aisle of the item: ");
                            namedItemsToUpdate[0].Aisle = int.Parse(Console.ReadLine());
                            Console.Write("Enter the updated Price of the item: ");
                            namedItemsToUpdate[0].Price = decimal.Parse(Console.ReadLine());
                            groceryLogic.UpdateGroceryItem(namedItemsToUpdate[0]);
                        }
                        else
                        {
                            foreach (var item in namedItemsToUpdate)
                            {
                                Console.WriteLine(JsonSerializer.Serialize(item));
                            }
                            Console.WriteLine();
                            Console.WriteLine($"Enter the Id of the {groceryItemToUpdateName} you would like to update.");
                            var groceryItemId = int.Parse(GetUserInput());
                            var namedItemsIds = namedItemsToUpdate.Select(x => x.GroceryItemId);
                            if (namedItemsIds.Contains(groceryItemId))
                            {
                                var itemToUpdate = groceryLogic.GetGroceryItemById(groceryItemId);
                                Console.WriteLine(JsonSerializer.Serialize(itemToUpdate));
                                Console.WriteLine();
                                Console.Write("Enter the updated Name of the item: ");
                                itemToUpdate.Name = Console.ReadLine();
                                Console.Write("Enter the updated Section of the item: ");
                                itemToUpdate.Section = Console.ReadLine();
                                Console.Write("Enter the updated Aisle of the item: ");
                                itemToUpdate.Aisle = int.Parse(Console.ReadLine());
                                Console.Write("Enter the updated Price of the item: ");
                                itemToUpdate.Price = decimal.Parse(Console.ReadLine());
                                groceryLogic.UpdateGroceryItem(itemToUpdate);
                            }
                            else Console.WriteLine($"There is no {namedItemsToUpdate} with Id {groceryItemId}.");
                        }
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine("What is the name of the grocery item you would like to remove? ");
                        var groceryItemToRemoveName = GetUserInput();
                        var namedItemsToRemove = groceryLogic.GetGroceryItemsByName(groceryItemToRemoveName);
                        if (namedItemsToRemove.Count == 0)
                            Console.WriteLine("We do not carry that item.");
                        else if (namedItemsToRemove.Count == 1) groceryLogic.RemoveGroceryItem(namedItemsToRemove[0]);
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
                            if (namedItemsIds.Contains(groceryItemId))
                            {
                                var itemToRemove = groceryLogic.GetGroceryItemById(groceryItemId);
                                groceryLogic.RemoveGroceryItem(itemToRemove);
                            }
                            else Console.WriteLine($"There is no {groceryItemToRemoveName} with Id {groceryItemId}.");
                        }
                        Console.WriteLine();
                        break;
                    case "back":
                        exitCondition = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

        static void DisplayGroceryItemMenu()
        {
            Console.WriteLine("Press 1 to View the Grocery Items");
            Console.WriteLine("Press 2 to View the Grocery Items by Section");
            Console.WriteLine("Press 3 to Add a Grocery Item as JSON");
            Console.WriteLine("Press 4 to Update a Grocery Item");
            Console.WriteLine("Press 5 to Remove a Grocery Item");
            Console.WriteLine("Type 'back' to return to the Main Menu");
            Console.Write("Choice: ");
        }

        static void GroceryListMenu(IGroceryLogic groceryLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplayGroceryListMenu();
                string choice = GetUserInput().ToLower();
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
                        if (namedItemsToAdd.Count == 0) Console.WriteLine("We do not carry that item.");
                        else if (namedItemsToAdd.Count == 1)
                        {
                            groceryLogic.AddItemToGroceryList(namedItemsToAdd[0]);
                            Console.WriteLine($"Added {groceryItemToAddName} to grocery items.");
                        }
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
                            if (namedItemsIds.Contains(groceryItemId))
                            {
                                groceryLogic.AddItemToGroceryListById(groceryItemId);
                                Console.WriteLine($"Added {groceryItemToAddName} to grocery items.");
                            }
                            else Console.WriteLine($"There is no {groceryItemToAddName} with Id {groceryItemId}.");
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("What is the name of the grocery item you would like to remove? ");
                        var groceryItemToRemoveName = GetUserInput();
                        var namedItemsToRemove = groceryLogic.GetGroceryItemsByName(groceryItemToRemoveName);
                        if (namedItemsToRemove.Count == 0)
                            Console.WriteLine("We do not carry that item.");
                        else if (namedItemsToRemove.Count == 1)
                        {
                            bool wasRemoved = groceryLogic.RemoveGroceryItemFromListById(namedItemsToRemove[0].GroceryItemId);
                            if (wasRemoved)
                                Console.WriteLine($"{groceryItemToRemoveName} was removed from your list.");
                            else
                                Console.WriteLine($"{groceryItemToRemoveName} was not in your list.");
                        }
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
                            if (namedItemsIds.Contains(groceryItemId))
                            {
                                bool wasRemoved = groceryLogic.RemoveGroceryItemFromListById(groceryItemId);
                                if (wasRemoved)
                                    Console.WriteLine($"{groceryItemToRemoveName} was removed from your list.");
                                else
                                    Console.WriteLine($"{groceryItemToRemoveName} was not in your list.");
                            }
                            else Console.WriteLine($"There is no {groceryItemToRemoveName} with Id {groceryItemId}.");
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine("The most expensive item in your grocery list is:");
                        Console.WriteLine();
                        var mostExpensiveItem = groceryLogic.GetMostExpensiveItemInList();
                        Console.WriteLine(JsonSerializer.Serialize(mostExpensiveItem));
                        Console.WriteLine();
                        break;
                    case "5":
                        string emailAddress = EmailLogic.GetValidEmailAddress();
                        Console.WriteLine($"Your email address is : {emailAddress}");
                        Console.WriteLine();
                        Console.WriteLine("The email did not send because this feature has not yet implemented.");
                        Console.WriteLine();
                        break;
                    case "6":
                        Task saveFileTask = SaveGroceryListLogic.SaveGroceryListToFile(groceryLogic.GetGroceryList());
                        Console.WriteLine("Your Grocery List was saved. Look for it in the bin as grocerylist.txt");
                        Console.WriteLine();
                        break;
                    case "back":
                        exitCondition = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
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
            Console.WriteLine("Press 5 to send your Grocery List to your email");
            Console.WriteLine("Press 6 to save your Grocery List to a text file");
            Console.WriteLine("Type 'back' to return to the Main Menu");
            Console.Write("Choice: ");
        }
    }
}

