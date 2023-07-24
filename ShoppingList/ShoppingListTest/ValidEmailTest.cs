using ShoppingList.Logic;

namespace ShoppingListTest;

[TestClass]
public class ValidEmailTest
{
    [TestMethod]
    public void Good_Email_Is_Valid()
    {
        // Arrange
        string goodEmail = "person@gmail.com";

        //Act
        bool isValid = EmailLogic.IsValidEmailAddress(goodEmail);

        //Assert
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void Bad_Email_Is_Not_Valid()
    {
        // Arrange
        string badEmail = "bademailatgmail.com";

        //Act
        bool isNotValid = EmailLogic.IsValidEmailAddress(badEmail);

        //Assert
        Assert.IsFalse(isNotValid);
    }
}
