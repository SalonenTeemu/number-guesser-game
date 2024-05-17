using System;

namespace NumberGuesser
{
    internal class Program
    {
        // Constants for minimum and maximum values for each game difficulty
        private const int min = 1;
        private const int easyMax = 10;
        private const int mediumMax = 20;
        private const int hardMax = 30;

        static void Main(string[] args)
        {
            GreetAndGiveInstructions();

            AskForDifficultyAndStartGame();
        }

        // Greet the user and tell the rules of the game
        static void GreetAndGiveInstructions() 
        {
            Console.WriteLine("Welcome to the number guesser game!");
            Console.WriteLine();
            Console.WriteLine("You have 3 tries to guess a number between a range.");
            Console.WriteLine("After a wrong guess, a tip is given telling if the correct number is greater or less than what was entered.");
            Console.WriteLine("Quit the game at any time by typing 'quit'.");
            Console.WriteLine();
        }

        // Run the game using selected difficulty
        static void StartGame(char difficulty, int max)
        {
            int triesLeft = 3;
            int correctNumber;
            Random rnd = new Random();
            Console.WriteLine();

            if (difficulty == 'E')
            {
                Console.WriteLine($"Easy difficulty chosen. Guessing numbers between {min}-{easyMax}.");
                correctNumber = rnd.Next(min, easyMax);
            } 
            else if (difficulty == 'M')
            {
                Console.WriteLine($"Medium difficulty chosen. Guessing numbers between {min}-{mediumMax}.");
                correctNumber = rnd.Next(min, easyMax);
            }
            else
            {
                Console.WriteLine($"Hard difficulty chosen. Guessing numbers between {min}-{hardMax}.");
                correctNumber = rnd.Next(min, hardMax);
            }

            int guess = 0;

            // Ask guesses until game is lost or won
            while (guess != correctNumber)
            {
                Console.WriteLine();
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                if (CheckQuitMessage(input)) return;

                // Make sure the inputted value is a number and between the selected range
                if (!int.TryParse(input, out guess) || guess < min || guess > max)
                {
                    PrintColorMessage(ConsoleColor.Red, $"Please enter a number between {min} and {max}.");
                    continue;
                }

                guess = Int32.Parse(input);

                if (guess != correctNumber)
                {
                    triesLeft--;
                    if (triesLeft > 0)
                    {
                        string triesMessage = triesLeft == 1 ? "try left." : "tries left.";
                        PrintColorMessage(ConsoleColor.Red, $"Wrong number, try again. {triesLeft} {triesMessage}");

                        if (correctNumber > guess)
                        {
                            PrintColorMessage(ConsoleColor.Yellow, "The correct number is greater than what was entered.");
                        }
                        else
                        {
                            PrintColorMessage(ConsoleColor.Yellow, "The correct number is less than what was entered.");
                        }
                        continue;
                    }
                    // Print a losing message and ask user if the game should be restarted
                    PrintColorMessage(ConsoleColor.Red, "WRONG!! You are out of tries and LOST!");
                    AskForRestart();
                    return;
                }
                else
                {
                    // Print a winning message and ask user if the game should be restarted
                    PrintColorMessage(ConsoleColor.Green, "CORRECT!! You guessed right and WON!");
                    AskForRestart();
                    return;
                }
            }
            AskForRestart();
            return;
        }

        // Ask the user to select a game difficulty and start the game with the chosen difficulty
        static void AskForDifficultyAndStartGame() 
        {
            Console.WriteLine("Please choose the difficulty level (1, 2 or 3):");
            Console.WriteLine($"1. Easy (Numbers between {min}-{easyMax})");
            Console.WriteLine($"2. Medium (Numbers between {min}-{mediumMax})");
            Console.WriteLine($"3. Hard (Numbers between {min}-{hardMax})");
            Console.WriteLine();

            int choice;

            while (true)
            {
                Console.Write("Enter the number corresponding to your choice: ");
                string input = Console.ReadLine();

                if (CheckQuitMessage(input)) return;

                bool isValidChoice = int.TryParse(input, out choice);

                if (isValidChoice)
                {
                    switch (choice)
                    {
                        case 1:
                            StartGame('E', easyMax);
                            return;
                        case 2:
                            StartGame('M', mediumMax);
                            return;
                        case 3:
                            StartGame('H', hardMax);
                            return;
                        default:
                            PrintColorMessage(ConsoleColor.Red, "Invalid choice. Please enter a number between 1 and 3.");
                            Console.WriteLine();
                            break;
                    }
                }
                else
                {
                    PrintColorMessage(ConsoleColor.Red, "Invalid input. Please enter a number.");
                    Console.WriteLine();
                    continue;
                }
            }
        }

        // Asks if the user wants to play again
        static void AskForRestart()
        {
            Console.WriteLine();
            Console.WriteLine("Do you want to play again? (Y or N)");
            Console.WriteLine();
            Console.Write("Play again?: ");

            string playAgainAnswer = Console.ReadLine().ToUpper();

            if (CheckQuitMessage(playAgainAnswer)) return;

            Console.WriteLine();

            if (playAgainAnswer == "Y")
            {
                AskForDifficultyAndStartGame();
                return;
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
                return;
            }
        }

        // Check if user entered message to quit the game
        static bool CheckQuitMessage(string input)
        {
            if (input.ToLower() == "quit")
            {
                Console.WriteLine();
                Console.WriteLine("Thanks for playing!");
                return true;
            }
            return false;
        }

        // Print a colored message and reset to the default color after
        static void PrintColorMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}
