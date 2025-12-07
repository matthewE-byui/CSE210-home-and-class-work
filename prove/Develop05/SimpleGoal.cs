using System;

namespace EternalQuest
{
    // A simple one-time goal that can be completed once
    public class SimpleGoal : Goal
    {
        private bool _isCompleted;

        public SimpleGoal(string name, string description, int points) : base(name, description, points)
        {
            _isCompleted = false;
        }

        public override void Display(int index)
        {
            string checkbox = _isCompleted ? "[X]" : "[ ]";
            Console.WriteLine($"{index}. {checkbox} {GetName()} ({GetDescription()})");
        }

        public override int RecordEvent()
        {
            if (_isCompleted)
            {
                Console.WriteLine("That goal is already completed. No points awarded.");
                return 0;
            }

            _isCompleted = true;
            Console.WriteLine($"Goal completed! You earned {_points} points.");
            return _points;
        }

        public override string GetStringRepresentation()
        {
            // SimpleGoal|name|description|points|isCompleted
            return $"SimpleGoal|{Escape(_name)}|{Escape(_description)}|{_points}|{_isCompleted}";
        }

        public override bool IsCompleted() => _isCompleted;

        private string Escape(string s) => s.Replace("|", "\\|");
        public static string Unescape(string s) => s.Replace("\\|", "|");

        // Factory for loading
        public static SimpleGoal FromParts(string[] parts)
        {
            // parts expected: [SimpleGoal, name, desc, points, isCompleted]
            string name = Unescape(parts[1]);
            string desc = Unescape(parts[2]);
            int pts = int.Parse(parts[3]);
            bool comp = bool.Parse(parts[4]);
            var g = new SimpleGoal(name, desc, pts);
            if (comp) g._isCompleted = true;
            return g;
        }
    }
}
