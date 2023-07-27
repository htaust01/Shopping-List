# Shopping List

## About

ShoppingList is a console application, developed with C# and .NET 7, that allows you to browse a grocery stores products and make a grocery list. It uses a database of grocery store products implemented with Entity Framework Core and SQLite. Each item in the database has an integer Id, a string Name, a string Section, and integer Aisle, and a decimal Price. You can view, add, update, and remove items from the product database. It also allows you to create a shopping list from the database. You can view your shopping list and total price, add items to your list (that are inserted in order by the items location in the store), and remove items from your list. The total price is updated as you add or remove items. There is an option to find the most expensive item in your list. There is an option to email the list to an email address, but the sending of the email is not functional.

## How to Use

You begin with a title screen and are given the choice to use the database (view, add, update, or remove), to use the grocery list (view, add, or remove), or exit the app.
If you choose use the database you have the options of viewing the database, viewing the database by section, adding an item to the database as JSON, updating an item in the database, removing an item from the database, or going back to the main menu.
If you choose to use the grocery list you have the options of viewing the grocery list, adding an item from the database to the grocery list, removing an item from the grocery list, finding the most expensive item in your grocery list, sending your grocery list by email (just validates email address, does not send it), save your grocery list as a text file, or going back to the main menu.
If you choose to exit the program ends.

## Features

- Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program : GroceryList is a class that contains a list of GroceryItem that is used throughout the app. The user can view, add to, or remove from the list and there is a method, GetMostExpensiveItemInList, that finds the most expensive item in the list. Also when an item is added to the list it is inserted by it's location in the grocery store.
- Implement a regular expression (regex) to ensure a field either a phone number or an email address is always stored and displayed in the same format : In the grocery list menu there is an option to email the grocery list. This does not actually send the email, but it does check the email address with regex to verify that it is a proper email address.
- Query your database using a raw SQL query, not EF : The methods GetGroceryItemsByName and GetGroceryItemsBySection in GroceryItemRepository both use FromSql to query the database using raw SQL.
- Create 3 or more unit tests for your application : Added a MSTest project that performs 3 tests, one to verify that the email validation rejects an invalid email address, one to verify that the email validation accepts a valid email address, and one to verify that the most expensive item on the grocery list is found by GetMostExpensiveItemInList method in GroceryLogic.
- Make your application asynchronous : You can save your grocery list as a text file using SaveGroceryListToFile, which is an asynchronous method.