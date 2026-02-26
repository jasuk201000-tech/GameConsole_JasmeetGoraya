using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameConsole_JasmeetGoraya
{
    // simple console game with authentication feature and games
    class Program
    {
        // different user roles -> impacts access control
        enum UserRole { Admin, User, Guest }

        static Dictionary<string, string> users = new Dictionary<string, string>()
        {
            // hard-coded example log ins
            { "admin", "1234" },
            { "jassi", "password" },
            { "mr feng", "letmein" }
        };

        static HashSet<string> admins = new HashSet<string>()
        {
            // ensuring that admin has access to admin tools
            "admin"
        };

        static void Main(string[] args)
        {
            // authentication menu returns the role and username of the logged in user
            var (role, username) = AuthMenu();

            bool isRunning = true;
            while (isRunning)
            {
                // main menu - depending on user type options will differ
                Console.Clear();
                Console.WriteLine("----- game console -----");
                Console.WriteLine("Logged in as: " + username);
                Task.Delay(300).Wait();

                Console.WriteLine("1) Rock Paper Scissors");
                Console.WriteLine("2) Naughts and Crosses");
                if (role == UserRole.Admin) Console.WriteLine("3) Admin tools (future)");
                Console.WriteLine("4) Exit");

                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                switch (input)
                {
                    // case based decision making- allowing for flexible input and less case sensitive errors
                    case "1":
                    case "one":
                        Play.Rps();
                        break;

                    case "2":
                    case "two":
                        Play.NaughtsAndCrosses();
                        break;

                    case "3":
                    case "three":
                        if (role == UserRole.Admin)
                            Console.WriteLine("Admin tools coming soon...");
                        else
                            Console.WriteLine("Only admin can access this.");
                        Task.Delay(1000).Wait();
                        break;

                    case "4":
                    case "four":
                        isRunning = false;
                        Console.WriteLine("Exiting... please press enter");
                        Task.Delay(800).Wait();
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Invalid input.");
                        Task.Delay(800).Wait();
                        break;
                }
            }
        }

        static (UserRole role, string username) AuthMenu()
        {
            while (true)
            {
                // authentication menu
                Console.Clear();
                Console.WriteLine("----- authentication -----");
                Console.WriteLine("1) Login");
                Console.WriteLine("2) Create account");
                Console.WriteLine("3) Continue as guest");
                Console.Write("Choose: ");

                string choice = (Console.ReadLine() ?? "").Trim().ToLower();

                if (choice == "3" || choice == "guest")
                    return (UserRole.Guest, "Guest");

                if (choice == "2" || choice == "create")
                {
                    CreateAccount();
                    continue;
                }

                if (choice == "1" || choice == "login")
                {
                    // when the log in is invalid or incorrect, the user is prompted to keep attempting or exit
                    var result = Login();
                    if (result.role != null) return (result.role.Value, result.username);

                    Console.WriteLine("Login failed. Press Enter...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Invalid option. Press Enter...");
                Console.ReadLine();
            }
        }

        static void CreateAccount()
        {
            // main create account dialouge
            Console.Clear();
            Console.WriteLine("----- create account -----");

            Console.WriteLine("Before proceeding, remember that both the username and password must be over 3 characters");
            Console.Write("New username: ");
            string username = (Console.ReadLine() ?? "").Trim();

            // if then statements, creating conditions for username and password - similar to modern day solutions
            if (username.Length < 3)
            {
                Console.WriteLine("Username must be at least 3 chars. Press Enter...");
                Console.ReadLine();
                return;
            }

            if (users.ContainsKey(username))
            {
                // ensuring that the username is not already in dictionary - preventing duplicates
                Console.WriteLine("That username already exists. Press Enter...");
                Console.ReadLine();
                return;
            }

            Console.Write("New password: ");
            string password = ReadPassword();
            Console.WriteLine();

            Console.Write("Confirm password: ");
            string confirm = ReadPassword();
            Console.WriteLine();

            if (password.Length < 4)
            {
                Console.WriteLine("Password must be at least 4 chars. Press Enter...");
                Console.ReadLine();
                return;
            }

            if (password.Contains(" "))
            {
                Console.WriteLine("Password cannot contain spaces. Press Enter...");
                Console.ReadLine();
                return;
            }

            if (password != confirm)
            {
                Console.WriteLine("Passwords don't match. Press Enter...");
                Console.ReadLine();
                return;
            }

            users[username] = password;
            Console.WriteLine("Account created! Press Enter...");
            Console.ReadLine();
        }

        static (UserRole? role, string username) Login()
        {
            int attempts = 3;

            while (attempts > 0)
            {
                Console.Clear();
                Console.WriteLine("----- login -----");

                Console.Write("Username: ");
                string username = (Console.ReadLine() ?? "").Trim();

                Console.Write("Password: ");
                string password = ReadPassword();
                Console.WriteLine();

                if (users.TryGetValue(username, out string storedPass) && storedPass == password)
                {
                    UserRole role = admins.Contains(username) ? UserRole.Admin : UserRole.User;
                    return (role, username);
                }

                attempts--;
                Console.WriteLine($"Wrong login. Attempts left: {attempts}");
                Task.Delay(900).Wait();
            }

            Console.WriteLine("Too many failed attempts. Returning to menu...");
            Task.Delay(2000).Wait();
            return (null, string.Empty);
        }

        private static string ReadPassword()
        {
            string pass = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (pass.Length > 0)
                    {
                        pass = pass[..^1]; // C# 8+
                        Console.Write("\b \b");
                    }
                    continue;
                }

                if (char.IsControl(key.KeyChar))
                    continue;

                pass += key.KeyChar;
                Console.Write("*");
            }

            return pass;
        }
    }

    class Play
    {
        // play class is where all the games are stored - each stored in its respective static bool
        private static bool PlayAgain()
        {
            while (true)
            {
                // asking user after each round if they'd like to play again
                Console.Write("Do you want to play again? (y/n): ");
                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;

                Console.WriteLine("Invalid input. Please type y or n.");
            }
        }

        public static void Rps()
        {
            // rock paper scissors game
            Console.Clear();
            Console.WriteLine("Welcome to rock paper scissors!");
            Console.Write("Please enter your name: ");
            string playerName = Console.ReadLine() ?? string.Empty;

            //
            int computerWin = 0;
            int playerWin = 0;
            int ties = 0;
            Random rng = new Random();

            bool keepPlaying = true;
            while (keepPlaying)
            {
                // main game loop dialouge
                Console.Clear();
                Console.WriteLine($"Player: {playerName}");
                Console.WriteLine($"Score -> Computer: {computerWin}  You: {playerWin}  Ties: {ties}");
                Console.WriteLine();
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

                if (playerChoice == computerChoice) { Console.WriteLine("It's a tie!"); ties++; }
                else if ((playerChoice == 1 && computerChoice == 3) ||
                         (playerChoice == 2 && computerChoice == 1) ||
                         (playerChoice == 3 && computerChoice == 2))
                { Console.WriteLine("You win!"); playerWin++; }
                else
                { Console.WriteLine("You lose!"); computerWin++; }

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