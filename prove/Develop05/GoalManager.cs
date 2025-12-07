using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Manages the list of goals and the player's score
    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        public void AddGoal(Goal g) => _goals.Add(g);

        public void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals yet.");
                return;
            }

            for (int i = 0; i < _goals.Count; i++)
            {
                _goals[i].Display(i + 1);
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Your score: {_score} points");
        }

        public bool RecordEvent(int index)
        {
            if (index < 0 || index >= _goals.Count) return false;
            var g = _goals[index];
            int earned = g.RecordEvent();
            _score += earned;
            return true;
        }

        public bool RemoveGoal(int index)
        {
            if (index < 0 || index >= _goals.Count) return false;
            _goals.RemoveAt(index);
            return true;
        }

        public bool EditGoal(int index, string newName, string newDesc)
        {
            if (index < 0 || index >= _goals.Count) return false;
            var g = _goals[index];
            
            if (!string.IsNullOrWhiteSpace(newName))
            {
                g.SetName(newName);
            }
            if (!string.IsNullOrWhiteSpace(newDesc))
            {
                g.SetDescription(newDesc);
            }
            return true;
        }

        public int GetGoalCount() => _goals.Count;

        public int GetCompletedCount()
        {
            int count = 0;
            foreach (var g in _goals)
            {
                if (g.IsCompleted()) count++;
            }
            return count;
        }

        public void SaveToFile(string filename)
        {
            var lines = new List<string>();
            lines.Add(_score.ToString());
            foreach (var g in _goals)
            {
                lines.Add(g.GetStringRepresentation());
            }
            File.WriteAllLines(filename, lines);
            Console.WriteLine($"Saved {_goals.Count} goals and score to '{filename}'.");
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File '{filename}' not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filename);
            if (lines.Length == 0)
            {
                Console.WriteLine("File empty or invalid.");
                return;
            }

            _goals.Clear();
            _score = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                // Need to split but honor escaped pipes '\|'. We'll split on '|' that are not escaped.
                var parts = SplitPreservingEscapes(line);
                if (parts.Length == 0) continue;

                string type = parts[0];
                try
                {
                    if (type == "SimpleGoal")
                        _goals.Add(SimpleGoal.FromParts(parts));
                    else if (type == "EternalGoal")
                        _goals.Add(EternalGoal.FromParts(parts));
                    else if (type == "ChecklistGoal")
                        _goals.Add(ChecklistGoal.FromParts(parts));
                    else
                        Console.WriteLine($"Unknown goal type '{type}' in file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load line: {line}. Error: {ex.Message}");
                }
            }

            Console.WriteLine($"Loaded {_goals.Count} goals. Score: {_score}");
        }

        // utility: split by '|' but allow escaped '\|'
        private static string[] SplitPreservingEscapes(string s)
        {
            var parts = new List<string>();
            var current = new System.Text.StringBuilder();
            bool escape = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (escape)
                {
                    // previous char was backslash; keep char (including '|')
                    current.Append(c);
                    escape = false;
                }
                else
                {
                    if (c == '\\')
                    {
                        escape = true;
                    }
                    else if (c == '|')
                    {
                        parts.Add(current.ToString());
                        current.Clear();
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
            }
            parts.Add(current.ToString());
            return parts.ToArray();
        }
    }
}
