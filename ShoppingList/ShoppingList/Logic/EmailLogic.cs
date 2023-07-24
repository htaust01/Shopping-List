using System;
using System.Text.RegularExpressions;

namespace ShoppingList.Logic
{
	static public class EmailLogic
	{
        static public string GetValidEmailAddress()
        {
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
            return emailAddress;
        }

        static public string GetEmailAddress()
        {
            Console.Write("Enter your email address: ");
            string emailAddress = Console.ReadLine();
            Console.WriteLine();
            return emailAddress;
        }

        static public bool IsValidEmailAddress(string emailAddress)
        {
            // Regex pattern for email
            // First match one or more a-z, A-Z, 0-9, . or - followed by an @
            // followed by one or more a-z, A-Z, 0-9 or - followed by a .
            // followed by 2-3 characters from a-z, A-Z, or 0-9 one or more times
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(emailAddress);
        }
    }
}

