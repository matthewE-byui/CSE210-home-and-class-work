using System;

namespace EternalQuest
{
    // An eternal goal can be recorded repeatedly and never completes
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points) : base(name, description, points)
        {
        }

        public override void Display(int index)
        {
            Console.WriteLine($"{index}. [∞] {GetName()} ({GetDescription()})");
        }

        public override int RecordEvent()
        {
            Console.WriteLine($"Recorded progress for '{GetName()}'. You earned {_points} points.");
            return _points;
        }

        public override string GetStringRepresentation()
        {
            // EternalGoal|name|description|points
            return $"EternalGoal|{Escape(_name)}|{Escape(_description)}|{_points}";
        }

        public override bool IsCompleted() => false;

        private string Escape(string s) => s.Replace("|", "\\|");
        public static string Unescape(string s) => s.Replace("\\|", "|");

        public static EternalGoal FromParts(string[] parts)
        {
            // parts: [EternalGoal, name, desc, points]
            string name = Unescape(parts[1]);
            string desc = Unescape(parts[2]);
            int pts = int.Parse(parts[3]);
            return new EternalGoal(name, desc, pts);
        }
    }
}
