using System;

class Program
{
    static void Main()
    {
        var activity = new Activity("Breathing", "This activity will help you relax and focus.");
        activity.DisplayStartingMessage();
        activity.ShowCountdown(3);
        activity.DisplayEndingMessage();
    }
}
