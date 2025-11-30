using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        int totalDuration = GetDuration();
        DateTime finishTime = DateTime.Now.AddSeconds(totalDuration);

        int stepSeconds = 4;

        while (DateTime.Now < finishTime)
        {
            Console.Write("\nBreathe in ... ");
            ShowCountdown(Math.Min(stepSeconds, (int)(finishTime - DateTime.Now).TotalSeconds));
            Console.Write("\nBreathe out ... ");
            ShowCountdown(Math.Min(stepSeconds, (int)(finishTime - DateTime.Now).TotalSeconds));
            Console.WriteLine();
        }
        DisplayEndingMessage();
    }
}