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
        do
            {   
                Console.WriteLine("Please enter a username: ");

                userInput = Console.ReadLine() ?? "";
                userInput = userInput.Trim();
                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Username cannot be blank, please try again.");
                    validInput = false;
                }else if(UserController.UserExists(userInput))
                {
                    Console.WriteLine("Username already exists, please choose another.");
                    validInput = false;
                }else{ 
                    UserController.CreateUser(userInput);
                    validInput = true;
                }
            } while (!validInput); 
    }

    public static void UserLoginMenu() 
    {
        bool validInput = true;
        string userInput = "";

        do
            {   
                Console.WriteLine("Please enter a username: ");

                userInput = Console.ReadLine() ?? "";
                userInput = userInput.Trim();

                if(String.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Username cannot be blank, please try again.");
                    validInput = false;
                }else if(!UserController.UserExists(userInput)) 
                {
                    Console.WriteLine("Username doesn't exist, please choose another.");
                    validInput = false;
                }else{ 
                    User existingUser = UserController.ReturnUser(userInput);
                    validInput = true;
                    
                    GameMenu.Start(existingUser);  //call game functionality menu
                }

            } while (!validInput); //Continue running the above block UNTIL input is valid
    }
}