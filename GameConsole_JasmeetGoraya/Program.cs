using System;
using System.Threading.Tasks;

namespace GameConsole_JasmeetGoraya
{
    class Program
    {
        // main menu for the game console, allows the user to choose which game they want to play and also allows them to exit the game console
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

                string userInput = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

                switch (userInput)
                {
                    case "1":
                    case "one":
                        Console.WriteLine("You have chosen to play rock paper scissors");
                        Task.Delay(1000).Wait();
                        Play.Rps();
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
        
        private static bool PlayAgain()
        {
            while (true)
            {
                Console.Write("Do you want to play again? (y/n): ");
                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;

                Console.WriteLine("Invalid input. Please type y or n.");
            }
        }

        
        public static void Rps()
        {
            Console.Clear();
            Console.WriteLine("Welcome to rock paper scissors!");

            Console.WriteLine("Please enter your name:");
            string playerName = Console.ReadLine() ?? string.Empty;

            int computerWin = 0;
            int playerWin = 0;
            int ties = 0;

            Random rng = new Random();

            bool keepPlaying = true;
            while (keepPlaying)
            {
                Console.Clear();
                Console.WriteLine($"Player: {playerName}");
                Console.WriteLine($"Score -> Computer: {computerWin}  You: {playerWin}  Ties: {ties}");
                Console.WriteLine();
                Console.WriteLine("Please enter any of the following:");
                Console.WriteLine("press one for rock");
                Console.WriteLine("press two for paper");
                Console.WriteLine("press three for scissors");

                int computerChoice = rng.Next(1, 4);

                string userInput = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

                int playerChoice;
                if (userInput == "1" || userInput == "one") playerChoice = 1;
                else if (userInput == "2" || userInput == "two") playerChoice = 2;
                else if (userInput == "3" || userInput == "three") playerChoice = 3;
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                    Task.Delay(1000).Wait();
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("You chose " + (playerChoice == 1 ? "rock" : playerChoice == 2 ? "paper" : "scissors"));
                Console.WriteLine("Computer chose " + (computerChoice == 1 ? "rock" : computerChoice == 2 ? "paper" : "scissors"));

                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("It's a tie!");
                    ties++;
                }
                else if (
                    (playerChoice == 1 && computerChoice == 3) ||
                    (playerChoice == 2 && computerChoice == 1) ||
                    (playerChoice == 3 && computerChoice == 2)
                )
                {
                    Console.WriteLine("You win!");
                    playerWin++;
                }
                else
                {
                    Console.WriteLine("You lose!");
                    computerWin++;
                }

                Console.WriteLine();
                keepPlaying = PlayAgain();
            }

            Console.WriteLine("Returning to menu...");
            Task.Delay(1000).Wait();
        }

        
        public static void NaughtsAndCrosses()
        {
            Console.Clear();
            Console.WriteLine("welcome to naughts and crosses");
            Task.Delay(500).Wait();

            bool keepPlaying = true;

            while (keepPlaying)
            {
                char currentPlayer = 'X';
                string[,] board = Create_Board();
                bool gameOver = false;

                while (!gameOver)
                {
                    Console.Clear();
                    Console.WriteLine("Current board:");
                    Display_Board(board);

                    Console.WriteLine();
                    Console.WriteLine("Player " + currentPlayer + ", enter your move (1-9):");
                    string move = Console.ReadLine() ?? string.Empty;

                    if (!Place_Move(board, move, currentPlayer))
                    {
                        Console.WriteLine("Invalid move, please try again.");
                        Task.Delay(1000).Wait();
                        continue;
                    }

                    if (Check_Winner(board, currentPlayer))
                    {
                        Console.Clear();
                        Console.WriteLine("Current board:");
                        Display_Board(board);

                        Console.WriteLine();
                        Console.WriteLine("Player " + currentPlayer + " wins!");
                        Console.WriteLine("Player " + (currentPlayer == 'X' ? 'O' : 'X') + " loses!");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();

                        gameOver = true;
                        keepPlaying = PlayAgain();
                    }
                    else if (Check_Draw(board))
                    {
                        Console.Clear();
                        Console.WriteLine("Current board:");
                        Display_Board(board);

                        Console.WriteLine();
                        Console.WriteLine("It's a draw!");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();

                        gameOver = true;
                        keepPlaying = PlayAgain();
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                }
            }

            Console.WriteLine("Returning to menu...");
            Task.Delay(1000).Wait();
        }

       

        private static string[,] Create_Board()
        {
            return new string[,]
            {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };
        }

        private static void Display_Board(string[,] board)
        {
            Console.WriteLine(board[0, 0] + " | " + board[0, 1] + " | " + board[0, 2]);
            Console.WriteLine("--+---+--");
            Console.WriteLine(board[1, 0] + " | " + board[1, 1] + " | " + board[1, 2]);
            Console.WriteLine("--+---+--");
            Console.WriteLine(board[2, 0] + " | " + board[2, 1] + " | " + board[2, 2]);
        }

        private static bool Place_Move(string[,] board, string move, char currentPlayer)
        {
            if (!int.TryParse(move, out int pos)) return false;
            if (pos < 1 || pos > 9) return false;

            int row = (pos - 1) / 3;
            int col = (pos - 1) % 3;

            if (board[row, col] == "X" || board[row, col] == "O") return false;

            board[row, col] = currentPlayer.ToString();
            return true;
        }

        private static bool Check_Winner(string[,] board, char player)
        {
            string p = player.ToString();

            for (int r = 0; r < 3; r++)
                if (board[r, 0] == p && board[r, 1] == p && board[r, 2] == p) return true;

            for (int c = 0; c < 3; c++)
                if (board[0, c] == p && board[1, c] == p && board[2, c] == p) return true;

            if (board[0, 0] == p && board[1, 1] == p && board[2, 2] == p) return true;
            if (board[0, 2] == p && board[1, 1] == p && board[2, 0] == p) return true;

            return false;
        }

        private static bool Check_Draw(string[,] board)
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board[r, c] != "X" && board[r, c] != "O")
                        return false;

            return true;
        }
    }
}