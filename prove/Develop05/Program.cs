using System;
using System.IO;

namespace EternalQuest
{
    class Program
    {
        private const string DEFAULT_SAVE_DIR = "goals_data";

        static void Main(string[] args)
        {
            // Ensure save directory exists
            if (!Directory.Exists(DEFAULT_SAVE_DIR))
            {
                Directory.CreateDirectory(DEFAULT_SAVE_DIR);
            }

            Console.WriteLine("Welcome to Eternal Quest!");
            var manager = new GoalManager();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n" + new string('=', 50));
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Record an event (complete a goal)");
                Console.WriteLine("4. Display score");
                Console.WriteLine("5. Display progress statistics");
                Console.WriteLine("6. Delete a goal");
                Console.WriteLine("7. Edit a goal");
                Console.WriteLine("8. Save goals to file");
                Console.WriteLine("9. Load goals from file");
                Console.WriteLine("10. Quit");
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal(manager);
                        break;
                    case "2":
                        manager.ListGoals();
                        break;
                    case "3":
                        RecordEvent(manager);
                        break;
                    case "4":
                        manager.DisplayScore();
                        break;
                    case "5":
                        DisplayProgress(manager);
                        break;
                    case "6":
                        DeleteGoal(manager);
                        break;
                    case "7":
                        EditGoal(manager);
                        break;
                    case "8":
                        SaveGoals(manager);
                        break;
                    case "9":
                        LoadGoals(manager);
                        break;
                    case "10":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("\nGoodbye!");
        }

        static void CreateGoal(GoalManager manager)
        {
            Console.WriteLine("\nChoose goal type:");
            Console.WriteLine("1. Simple goal (one-time)");
            Console.WriteLine("2. Eternal goal (repeatable)");
            Console.WriteLine("3. Checklist goal (complete N times)");
            Console.Write("Choice: ");
            string? type = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string? name = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Goal name cannot be empty.");
                return;
            }

            Console.Write("Enter goal description: ");
            string? desc = Console.ReadLine() ?? "";
            Console.Write("Enter points awarded per completion: ");
            int points = ReadIntFromConsole(1);

            if (type == "1")
            {
                var sg = new SimpleGoal(name, desc, points);
                manager.AddGoal(sg);
                Console.WriteLine("✓ Simple goal created successfully.");
            }
            else if (type == "2")
            {
                var eg = new EternalGoal(name, desc, points);
                manager.AddGoal(eg);
                Console.WriteLine("✓ Eternal goal created successfully.");
            }
            else if (type == "3")
            {
                Console.Write("How many times required to complete? ");
                int required = ReadIntFromConsole(1);
                Console.Write("Bonus points for completion: ");
                int bonus = ReadIntFromConsole(0);
                var cg = new ChecklistGoal(name, desc, points, required, bonus);
                manager.AddGoal(cg);
                Console.WriteLine("✓ Checklist goal created successfully.");
            }
            else
            {
                Console.WriteLine("Unknown goal type - cancelled.");
            }
        }

        static void RecordEvent(GoalManager manager)
        {
            if (manager.GetGoalCount() == 0)
            {
                Console.WriteLine("No goals available. Create a goal first.");
                return;
            }

            Console.WriteLine("\nChoose a goal to record:");
            manager.ListGoals();
            Console.Write("Enter goal number (or 0 to cancel): ");
            int index = ReadIntFromConsole(0);
            
            if (index == 0)
                return;

            if (!manager.RecordEvent(index - 1))
            {
                Console.WriteLine("Invalid goal selection.");
            }
        }

        static void DeleteGoal(GoalManager manager)
        {
            if (manager.GetGoalCount() == 0)
            {
                Console.WriteLine("No goals available.");
                return;
            }

            Console.WriteLine("\nChoose a goal to delete:");
            manager.ListGoals();
            Console.Write("Enter goal number (or 0 to cancel): ");
            int index = ReadIntFromConsole(0);
            
            if (index == 0)
                return;

            if (manager.RemoveGoal(index - 1))
            {
                Console.WriteLine("✓ Goal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid goal selection.");
            }
        }

        static void EditGoal(GoalManager manager)
        {
            if (manager.GetGoalCount() == 0)
            {
                Console.WriteLine("No goals available.");
                return;
            }

            Console.WriteLine("\nChoose a goal to edit:");
            manager.ListGoals();
            Console.Write("Enter goal number (or 0 to cancel): ");
            int index = ReadIntFromConsole(0);
            
            if (index == 0)
                return;

            Console.Write("Enter new goal name (press Enter to skip): ");
            string? newName = Console.ReadLine();
            
            Console.Write("Enter new description (press Enter to skip): ");
            string? newDesc = Console.ReadLine();

            if (manager.EditGoal(index - 1, newName ?? "", newDesc ?? ""))
            {
                Console.WriteLine("✓ Goal updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid goal selection.");
            }
        }

        static void DisplayProgress(GoalManager manager)
        {
            if (manager.GetGoalCount() == 0)
            {
                Console.WriteLine("No goals yet.");
                return;
            }

            int completed = manager.GetCompletedCount();
            int total = manager.GetGoalCount();
            double percentage = (double)completed / total * 100;

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("PROGRESS STATISTICS");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Total Goals: {total}");
            Console.WriteLine($"Completed Goals: {completed}");
            Console.WriteLine($"Completion Rate: {percentage:F1}%");
            Console.WriteLine($"Current Score: ");
            manager.DisplayScore();
            Console.WriteLine(new string('=', 50));
        }

        static void SaveGoals(GoalManager manager)
        {
            Console.Write("\nEnter filename (or press Enter for default 'goals.txt'): ");
            string? input = Console.ReadLine();
            string filename = string.IsNullOrWhiteSpace(input) ? "goals.txt" : input;
            string fullPath = Path.Combine(DEFAULT_SAVE_DIR, filename);
            manager.SaveToFile(fullPath);
        }

        static void LoadGoals(GoalManager manager)
        {
            // List available files
            if (Directory.Exists(DEFAULT_SAVE_DIR))
            {
                var files = Directory.GetFiles(DEFAULT_SAVE_DIR, "*.txt");
                if (files.Length > 0)
                {
                    Console.WriteLine("\nAvailable files:");
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                    }
                }
            }

            Console.Write("\nEnter filename to load: ");
            string? filename = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(filename))
            {
                string fullPath = Path.Combine(DEFAULT_SAVE_DIR, filename);
                manager.LoadFromFile(fullPath);
            }
        }

        static int ReadIntFromConsole(int minValue = 0)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int n))
                {
                    if (n >= minValue)
                        return n;
                    else
                        Console.Write($"Please enter a valid integer (minimum {minValue}): ");
                }
                else
                    Console.Write("Please enter a valid integer: ");
            }
        }
    }
}
