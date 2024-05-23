using System.Runtime.CompilerServices;
using System.Text;
using NoughtsAndCrosses.Controllers;
using NoughtsAndCrosses.Models;

namespace NoughtsAndCrosses.Presentation;

public class Menu
{
    public static void StartMenu() 
    {

        int userChoice = 0;
        bool validInput = true;

        Console.Clear();
        Console.WriteLine("\nNOUGHTS AND CROSSES\n");
        Console.WriteLine("\nPlease login to play Noughts and Crosses\n");
        Console.WriteLine("1. Login using your existing account");
        Console.WriteLine("2. Create a new account");
        Console.WriteLine("3. Exit\n");
        Console.Write(": ");

        do
            {
                try
                {
                    userChoice = Convert.ToInt32(Console.ReadLine());
                    validInput = true;

                    switch (userChoice)
                    {
                        case 1: // returning user
                            UserLoginMenu();
                            break;
                        case 2:
                            UserCreationMenu();
                            break;

                        case 3: 
                            return; //This return exits this method, and returns us to where it was called.

                        default: // If the user enters an integer that is not 1, 2, or 3
                            Console.WriteLine("Please enter a valid choice.");
                            validInput = false;
                            break;
                    }
                }
                catch (Exception ex) 
                {   
                    validInput = false;
                    Console.WriteLine("Please enter a valid choice!");
                }
            } while (!validInput);
    }

    public static void UserCreationMenu() 
    {
        bool validInput = true;
        string userInput = "";

        Console.Clear();
        Console.WriteLine("\nNOUGHTS AND CROSSES - Create a New Account\n\n");

        do
            {   
                Console.Write("Please enter a username: ");

                userInput = Console.ReadLine() ?? "";
                userInput = userInput.Trim();
                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Username cannot be blank, please try again.");
                    validInput = false;
                }
                else if (userInput.Length > 20)
                {
                    Console.WriteLine("Username cannot be longer than 20 characters. Please try again.");
                    validInput = false;
                } 
                else if(UserController.UserExists(userInput))
                {
                    Console.WriteLine("Username already exists, please choose another.");
                    validInput = false;
                }else
                { 
                    string password = GetInitialPasswordFromUser();
                    validInput = true;

                    User newUser = UserController.CreateUser(userInput, password);
                    GameMenu.Start(newUser);
                }
            } while (!validInput);       
    }

    public static void UserLoginMenu() 
    {
        bool validInput = true;
        string userInput = "";

        Console.Clear();
        Console.WriteLine("\nNOUGHTS AND CROSSES - Logging in with Existing Account\n\n");
        do
            {   
                Console.Write("Please enter your username: ");

                userInput = Console.ReadLine() ?? "";
                userInput = userInput.Trim();

                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Username cannot be blank, please try again.");
                    validInput = false;
                }
                else if(!UserController.UserExists(userInput)) 
                {
                    Console.WriteLine("Username doesn't exist, please choose another.");
                    validInput = false;
                }else{ 
                    User existingUser = UserController.ReturnUser(userInput);
                    validInput = true;

                    if (!canUserEnterTheCorrectPassword(existingUser))
                        return;
                                           
                    GameMenu.Start(existingUser); 
                }

            } while (!validInput); 
    }

    public static string GetInitialPasswordFromUser()
    {
        string password1 = "";
        string password2 = "";
        bool passwordsMatch = false;

        while (!passwordsMatch)
        {
            Console.Write("Please enter the password that you want to use: ");
            password1 = GetPasswordFromUser();
            Console.Write("Now re-enter the same password as verification: ");
            password2 = GetPasswordFromUser();
            passwordsMatch = password1 == password2;
            if (!passwordsMatch)
                Console.WriteLine("\nThe passwords do not match. Let's try this again.\n");
        }

        return password1;
    }

    public static string GetPasswordFromUser()
    //
    // allow the user to enter a string without it appearing on the screen.
    // we assume that the calling code has already prompted for entry.
    //
    {
        StringBuilder password = new StringBuilder();
        string maskCharacter = "*";

        while (true)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
            if (keyPress.Key == ConsoleKey.Backspace && password.Length > 0)  // handle backspace
                {
                    password.Remove(password.Length - 1, 1);    // remove last character from input
                    Console.SetCursorPosition(x - 1, y);        // move cursor left by one character
                    Console.Write(" ");                         // replace whatever is there by a space
                    Console.SetCursorPosition(x - 1, y);        // move cursor left by one character
                }
            else if (keyPress.Key < ConsoleKey.Spacebar || keyPress.KeyChar > '~')
                {
                    // ignore the key press if the character is outside the normal ascii range,
                    // if it's less than a space or greater than a tilde.
                }
            else
                {
                    password.Append(keyPress.KeyChar);
                    Console.Write(maskCharacter);
                }
        }
        return password.ToString();
    }    

    public static bool canUserEnterTheCorrectPassword(User user)
    {
        string password = "";
        bool passwordIsCorrect = false;
        int availableAttempts = 3;

        Console.Write("Enter password: ");
        while (!passwordIsCorrect && availableAttempts > 0)
        {
            password = GetPasswordFromUser();
            passwordIsCorrect = UserController.IsUserPasswordCorrect(user, password);
            if (!passwordIsCorrect)
            {
                Console.Write("Incorrect Password. Try again: ");   
                availableAttempts--;
            }
        }
        if (!passwordIsCorrect)
            {
                Console.WriteLine("\n\nToo many attempts. Access is denied.\n");
            }
        return passwordIsCorrect;
    }
}