using System;
using System.Threading;

public class Activity
{
    protected string _name;
    protected string _description;
    private int _duration; // seconds

    public Activity(string name = "", string description = "")
    {
        _name = name;
        _description = description;
    }

    public void SetDuration(int seconds)
    {
        if (seconds < 0) seconds = 0;
        _duration = seconds;
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---\n");
        Console.WriteLine(_description + "\n");
        Console.WriteLine("Enter duration in seconds: ");
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int seconds) && seconds >= 0)
            {
                SetDuration(seconds);
                break;
            }
            Console.WriteLine("Please enter a non-negative integer value for seconds: ");
        }
        Console.WriteLine("\nGet ready to start...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name} activity.");
        ShowSpinner(2);
    }

    // spinner animation
    public void ShowSpinner(int seconds)
    {
        string[] symbols = {"|", "/", "-", "\\"};
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write(symbols[i]);
            Thread.Sleep(200);
            Console.Write("\b \b");
            i = (i + 1) % symbols.Length;
        }
    }

    // countdown from seconds to 1 (prints in place)
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string text = i.ToString();
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            Console.Write(text);
            Thread.Sleep(1000);
            // Erase the printed number without disturbing other text
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', text.Length));
            Console.SetCursorPosition(left, top);
        }
    }

    // Virtual so derived classes override their own behavior
    public virtual void Run()
    {
    }
}
