using System;
using System.Collections.Generic;
using System.Threading;

public class ReflectingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless.",
        "Think of a time when you learned a valuable lesson (good or bad)."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private Random _rnd = new Random();

    public ReflectingActivity() : base(
        "Reflecting Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }
    private string GetRandomPrompt()
    {
        int i = _rnd.Next(_prompts.Count);
        return _prompts[i];
    }

    private string GetRandomQuestion()
    {
        int i = _rnd.Next(_questions.Count);
        return _questions[i];
    }

    public override void Run()
    {
        DisplayStartingMessage();
        int totalSeconds = GetDuration();
        DateTime finishTime = DateTime.Now.AddSeconds(totalSeconds);

        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.WriteLine("\nWhen you are ready, reflect on the following questions:");
        ShowSpinner(2);

        while (DateTime.Now < finishTime)
        {
            string q = GetRandomQuestion();
            Console.Write("\n" + q);
            // Pause for thought and show spinner for a few seconds
            ShowSpinner(4);
        }
        DisplayEndingMessage();
    }
}