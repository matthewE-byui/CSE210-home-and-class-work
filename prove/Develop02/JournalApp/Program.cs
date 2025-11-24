using System;
using System.IO;

namespace JournalApp
{
    class Program
    {
        // - Clean, organized menu interface with numbered options
        // - Proper entry count display and empty journal handling
        // - File operation confirmations and error handling
        // - Separation of concerns with distinct menu flows

        static void Main(string[] args)
        {
            Journal myJournal = new Journal();
            PromptGenerator promptGen = new PromptGenerator();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Quit");
                Console.Write("Select an option (1-5): ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        string prompt = promptGen.GetRandomPrompt();
                        Console.WriteLine($"\nPrompt: {prompt}");
                        Console.Write("Your response: ");
                        string? response = Console.ReadLine();
                        if (response != null)
                        {
                            string dateTime = DateTime.Now.ToString("g");
                            Entry newEntry = new Entry(dateTime, prompt, response);
                            myJournal.AddEntry(newEntry);
                            Console.WriteLine("Entry saved!\n");
                        }
                        break;

                    case "2":
                        myJournal.DisplayAll();
                        break;

                    case "3":
                        Console.Write("Enter filename to save to (name only: ");
                        string? saveFile = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(saveFile))
                        {
                            string filename = saveFile.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                                ? saveFile
                                : saveFile + ".txt";

                            myJournal.SaveToFile(filename);
                        }
                        break;

                    case "4":
                        Console.WriteLine("\nAvailable journal files:");
                        string savedJournalsPath = Path.Combine("saved_journals");
                        string[] txtFiles = Directory.Exists(savedJournalsPath)
                            ? Directory.GetFiles(savedJournalsPath, "*.txt")
                            : new string[0];
                        
                        if (txtFiles.Length == 0)
                        {
                            Console.WriteLine("No journal files found.");
                            break;
                        }

                        for (int i = 0; i < txtFiles.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
                        }

                        Console.Write("\nSelect a file number (or enter custom filename): ");
                        string? selection = Console.ReadLine();
                        
                        if (int.TryParse(selection, out int fileIndex) && fileIndex > 0 && fileIndex <= txtFiles.Length)
                        {
                            string selectedFile = txtFiles[fileIndex - 1];
                            myJournal.LoadFromFile(Path.GetFileName(selectedFile));
                            myJournal.DisplayAll();
                        }
                        else if (!string.IsNullOrEmpty(selection))
                        {
                            myJournal.LoadFromFile(selection);
                            myJournal.DisplayAll();
                        }
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
// I added my own changes to some of the text in the code so it would be more fun and engaging.
// I also made sure that the menu interface is clean and easy to navigate.
// the prompt questions were also modified to be more thought-provoking and interesting.
// the date and time are now included in each journal entry for better tracking.
// i added a micro setting so you can choose journals.
// changed things up so you can write simple names for files without needing to type .txt every time.
//testing push
