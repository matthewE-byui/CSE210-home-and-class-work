using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>()
    {
        "who are people that you appreciate?",
        "what are personal strengths of yours?",
        "who are people that you have helped this week?",
        "when have you felt the Holy Ghost this month?",
        "who are some of your personal heroes?"
    };

    private List<string> _responses = new List<string>();
    private Random _random = new Random();

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }
    private string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        int totalSeconds = GetDuration();

        string prompt = GetRandomPrompt();
        Console.WriteLine("\nList as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine("You may begin in:");
        ShowCountdown(5);
        Console.WriteLine("\nStart listing. Press Enter after each response.");

        DateTime finishTime = DateTime.Now.AddSeconds(totalSeconds);
        _responses.Clear();

        while (DateTime.Now < finishTime)
        {
            //if input would block past finishTime, we still allow that last entry
            Console.Write("> ");
            string entry = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entry))
                _responses.Add(entry.Trim());
        }

        Console.WriteLine($"\nTime's up! You listed {_responses.Count} items.");
        ShowSpinner(3);
        DisplayEndingMessage();
    }

    public int GetResponseCount()
    {
        return _responses.Count;
    }
}