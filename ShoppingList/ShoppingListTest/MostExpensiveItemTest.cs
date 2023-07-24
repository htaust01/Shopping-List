using ShoppingList.Logic;
using ShoppingList.Data;

namespace ShoppingListTest;

[TestClass]
public class MostExpensiveItemTest
{
    [TestMethod]
    public void GetMostExpensiveItemInList_Returns_Most_Expensive_Item_In_List()
    {
        // Arrange
        GroceryList testList = new GroceryList()
        {
            GroceryItems =
            {
                new GroceryItem()
                {
                    Name = "Test1",
                    Section = "Grocery",
                    Aisle = 1,
                    Price = 1.00m
                },
                new GroceryItem()
                {
                    Name = "Test2",
                    Section = "Grocery",
                    Aisle = 1,
                    Price = 100.00m
                },
                new GroceryItem()
                {
                    Name = "Test3",
                    Section = "Grocery",
                    Aisle = 1,
                    Price = 10.00m
                }
            },
            TotalPrice = 111.00m
        };

        //Act
        decimal expectedPrice = 100.00m;
        var groceryLogic = new GroceryLogic(new GroceryItemRepository());
        groceryLogic._groceryList = testList;
        var actualPrice = groceryLogic.GetMostExpensiveItemInList().Price;

        //Assert
        Assert.AreEqual(expectedPrice, actualPrice);
    }
}
