using System;

namespace EternalQuest
{
    // A checklist goal requires N completions to be considered finished.
    public class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _requiredCount;
        private int _bonusPoints;

        public ChecklistGoal(string name, string description, int points, int requiredCount, int bonusPoints) 
            : base(name, description, points)
        {
            _timesCompleted = 0;
            _requiredCount = requiredCount;
            _bonusPoints = bonusPoints;
        }

        public override void Display(int index)
        {
            string checkbox = IsCompleted() ? "[X]" : "[ ]";
            Console.WriteLine($"{index}. {checkbox} {GetName()} ({GetDescription()}) -- Completed {_timesCompleted}/{_requiredCount}");
        }

        public override int RecordEvent()
        {
            if (IsCompleted())
            {
                Console.WriteLine("This checklist goal is already complete. No points awarded.");
                return 0;
            }

            _timesCompleted++;
            int awarded = _points;
            if (_timesCompleted >= _requiredCount)
            {
                // goal finished — award bonus and mark as complete
                awarded += _bonusPoints;
                Console.WriteLine($"Goal completed! You earned {_points} + bonus {_bonusPoints} = {awarded} points.");
            }
            else
            {
                Console.WriteLine($"Progress recorded! You earned {_points} points. ({_timesCompleted}/{_requiredCount})");
            }
            return awarded;
        }

        public override string GetStringRepresentation()
        {
            // ChecklistGoal|name|description|points|timesCompleted|requiredCount|bonusPoints
            return $"ChecklistGoal|{Escape(_name)}|{Escape(_description)}|{_points}|{_timesCompleted}|{_requiredCount}|{_bonusPoints}";
        }

        public override bool IsCompleted() => _timesCompleted >= _requiredCount;

        private string Escape(string s) => s.Replace("|", "\\|");
        public static string Unescape(string s) => s.Replace("\\|", "|");

        public static ChecklistGoal FromParts(string[] parts)
        {
            // parts: [ChecklistGoal, name, desc, points, timesCompleted, requiredCount, bonusPoints]
            string name = Unescape(parts[1]);
            string desc = Unescape(parts[2]);
            int pts = int.Parse(parts[3]);
            int times = int.Parse(parts[4]);
            int req = int.Parse(parts[5]);
            int bonus = int.Parse(parts[6]);
            var g = new ChecklistGoal(name, desc, pts, req, bonus);
            g._timesCompleted = times;
            return g;
        }
    }
}
