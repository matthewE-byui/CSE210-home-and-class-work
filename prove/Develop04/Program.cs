using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("-------------------");
            Console.WriteLine("1) Breathing Activity");
            Console.WriteLine("2) Reflecting Activity");
            Console.WriteLine("3) Listing Activity");
            Console.WriteLine("4) Quit");
            Console.Write("\nChoose an option: ");

            string choice = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(choice)) continue;

            if (choice == "4" || choice.Equals("quit", StringComparison.OrdinalIgnoreCase))
                break;

            Activity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectingActivity(),
                "3" => new ListingActivity(),
                _ => null
            };

            if (activity == null)
            {
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();
                continue;
            }

            activity.Run();

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }
}

/*
Stretch notes (include in Program.cs comments for submission):
- I implemented randomized prompts/questions and a simple spinner/countdown animation.
- ListingActivity times by DateTime and counts responses entered during the duration (core requirement).
- To exceed core requirements you could:
   * Persist activity logs to a file (not implemented).
   * Ensure each prompt/question is used once before repeating (not implemented).
*/
