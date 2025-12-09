using System;
using FinalProject.Engine;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        
        CommandEngine engine = new CommandEngine();
        bool running = true;

        while (running)
        {
            DisplayPrompt();
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string response = engine.Process(input);

            if (response == "EXIT")
            {
                running = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n{response}\n");
                Console.ResetColor();
            }
        }

        DisplayGoodbye();
    }

    static void DisplayWelcome()
    {
        try
        {
            Console.Clear();
        }
        catch
        {
            // Console.Clear() may fail in piped output
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║     JARVIS - Advanced Command Engine     ║");
        Console.WriteLine("║         Your Personal AI Assistant       ║");
        Console.WriteLine("╚══════════════════════════════════════════╝\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Type 'help' to see all available commands");
        Console.WriteLine("Type 'exit' or 'quit' to close the application\n");
        Console.ResetColor();
    }

    static void DisplayPrompt()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("jarvis> ");
        Console.ResetColor();
    }

    static void DisplayGoodbye()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n╔══════════════════════════════════════════╗");
        Console.WriteLine("║    Thank you for using JARVIS! Goodbye!  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
    }
}
