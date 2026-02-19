using System;
using System.Threading.Tasks;

namespace GameConsole_JasmeetGoraya
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("----- game console -----");
                Task.Delay(500).Wait();
                Console.WriteLine("Press one to play rock paper scissors");
                Task.Delay(500).Wait();
                Console.WriteLine("Press two to play naughts and crosses");
                Task.Delay(500).Wait();
                Console.WriteLine("Press three to return back to home screen");
                Task.Delay(500).Wait();
                Console.WriteLine("Press four to exit the game console");

                string userInput = Console.ReadLine() ?? string.Empty;
                string userInputLower = userInput.ToLower();

                switch (userInputLower)
                {
                    case "1":
                    case "one":
                        Console.WriteLine("You have chosen to play rock paper scissors");
                        Task.Delay(1000).Wait();
                        Play.rps();
                        break;

                    case "2":
                    case "two":
                        Console.WriteLine("You have chosen to play naughts and crosses");
                        Task.Delay(1000).Wait();
                        Play.NaughtsAndCrosses(); 
                        break;

                    case "3":
                    case "three":
                        Console.WriteLine("Returning back to home screen...");
                        Task.Delay(1000).Wait();
                        break;

                    case "4":
                    case "four":
                        Console.WriteLine("Exiting the game console...");
                        Task.Delay(1000).Wait();
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        Task.Delay(1000).Wait();
                        break;
                }
            }
        }
    }

    class Play
    {
        public static void rps()
        {
            Console.Clear();
            Console.WriteLine("Welcome to rock paper scissors!");

            int rock = 1;
            int paper = 2;
            int scissors = 3;

            int computerWin = 0;
            int playerWin = 0;
            int ties = 0;

            Random rng = new Random();
            int computerChoice = rng.Next(0, 3);

          

            Console.WriteLine("Please enter any of the following:");
            Console.WriteLine("press one for rock");
            Console.WriteLine("press two for paper"); 
            Console.WriteLine("press three for scissors");
            string userInput = Console.ReadLine() ?? string.Empty;

            if (userInput == "1" || userInput.ToLower() == "one")
            {
                Console.WriteLine("You chose rock");
                if (computerChoice == rock)
                {
                    Console.WriteLine("The computer chose rock, it's a tie!");
                    ties = ties + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else if (computerChoice == paper)
                {
                    Console.WriteLine("The computer chose paper, you lose!");
                    computerWin = computerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else
                {
                    Console.WriteLine("The computer chose scissors, you win!");
                    playerWin = playerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
            }
            else if (userInput == "2" || userInput.ToLower() == "two")
            {
                Console.WriteLine("You chose paper");
                if (computerChoice == rock)
                {
                    Console.WriteLine("The computer chose rock, you win!");
                    ties = ties + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else if (computerChoice == paper)
                {
                    Console.WriteLine("The computer chose paper, it's a tie!");
                    computerWin = computerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else
                {
                    Console.WriteLine("The computer chose scissors, you lose!");
                    computerWin = computerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
            }
            else if (userInput == "3" || userInput.ToLower() == "three")
            {
                Console.WriteLine("You chose scissors");
                if (computerChoice == rock)
                {
                    Console.WriteLine("The computer chose rock, you lose!");
                    ties = ties + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else if (computerChoice == paper)
                {
                    Console.WriteLine("The computer chose paper, you win!");
                    computerWin = computerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
                else
                {
                    Console.WriteLine("The computer chose scissors, it's a tie!");
                    computerWin = computerWin + 1;
                    Console.WriteLine("Computer: " + computerWin + "Player: " + playerWin + "Ties: " + ties);
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please try again.");
            }



            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();

           
        }
        
        public static void NaughtsAndCrosses()
        {
            // todo: implement naughts and crosses game
        }
    }
