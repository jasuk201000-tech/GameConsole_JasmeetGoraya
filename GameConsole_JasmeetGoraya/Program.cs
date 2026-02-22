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

            Console.WriteLine("Please enter your name:");
            string playerName = Console.ReadLine() ?? string.Empty;

            int computerWin = 0;
            int playerWin = 0;
            int ties = 0;
            bool playAgain = true;

            

        Random rng = new Random();

        
            int computerChoice = rng.Next(1, 4);

            Console.WriteLine("Please enter any of the following:");
            Console.WriteLine("press one for rock");
            Console.WriteLine("press two for paper");
            Console.WriteLine("press three for scissors");
            string userInput = Console.ReadLine() ?? string.Empty;

            
            if (userInput == "1" || userInput.ToLower() == "one")
            {
                Console.WriteLine("You chose rock");
                if (computerChoice == rock) { Console.WriteLine("The computer chose rock, it's a tie!"); ties++; }
                else if (computerChoice == paper) { Console.WriteLine("The computer chose paper, you lose!"); computerWin++; }
                else { Console.WriteLine("The computer chose scissors, you win!"); playerWin++; }
                PlayAgain();
            }
            else if (userInput == "2" || userInput.ToLower() == "two")
            {
                Console.WriteLine("You chose paper");
                if (computerChoice == rock) { Console.WriteLine("The computer chose rock, you win!"); playerWin++; }
                else if (computerChoice == paper) { Console.WriteLine("The computer chose paper, it's a tie!"); ties++; }
                else { Console.WriteLine("The computer chose scissors, you lose!"); computerWin++; }
                PlayAgain();
            }
            else if (userInput == "3" || userInput.ToLower() == "three")
            {
                Console.WriteLine("You chose scissors");
                if (computerChoice == rock) { Console.WriteLine("The computer chose rock, you lose!"); computerWin++; }
                else if (computerChoice == paper) { Console.WriteLine("The computer chose paper, you win!"); playerWin++; }
                else { Console.WriteLine("The computer chose scissors, it's a tie!"); ties++; }
                PlayAgain();
            }
            else
            {
                Console.WriteLine("Invalid input, please try again.");
            }

            Console.WriteLine("Computer: " + computerWin + " Player: " + playerWin + " Ties: " + ties);
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
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

                    if (Place_Move(board, move, currentPlayer))
                    {
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

                            PlayAgain();
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

                            PlayAgain();
                        }

                        else
                        {
                            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid move, please try again.");
                        Task.Delay(1000).Wait();
                    }
                }
            }
        }

        private static void PlayAgain()
        {
            Console.WriteLine("Do you want to play again? (y/n)");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.ToLower() == "y" || input.ToLower() == "yes")
            {
                NaughtsAndCrosses();
            }
            else if (input.ToLower() == "n" || input.ToLower() == "no")
            {
                Console.WriteLine("Returning to menu...");
                Task.Delay(1000).Wait();
            }
            else
            {
                Console.WriteLine("Invalid input, returning to menu...");
                Task.Delay(1000).Wait();
            }
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
