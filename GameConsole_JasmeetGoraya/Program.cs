using System;  
using System.Threading.Tasks;


namespace GameConsole_JasmeetGoraya
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning) {
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
                        Play.rps();
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
}



