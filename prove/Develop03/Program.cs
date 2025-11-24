using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        // Extra features implemented beyond core requirements:
        // - Words preserve punctuation when hidden (only letters/digits are replaced by underscores).
        // - Hides up to N distinct visible words per Enter, so it won't re-hide already-hidden words.
        // - Uses a shared Random to avoid predictable patterns.
        // - GetDisplayText prints the reference and the current word states.
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        Scripture scripture = new Scripture(reference,
            "Trust in the Lord with all thine heart and lean not unto thine own understanding");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input != null && input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are now hidden. Program ending...");
                break;
            }
        }
    }
}
