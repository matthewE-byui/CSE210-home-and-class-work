using System;

namespace EternalQuest
{
    // Base class for all goal types 
    public abstract class Goal
    {
        // protected so derived classes can access directly if needed 
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        // Display the goal in a readable way
        public abstract void Display(int index);

        // Records an event for this goal and returns points awarded by the event
        // Overide in derived classes to implement specific behavior
        public abstract int RecordEvent();

        // return a string representation suitable for saving to a file
        public abstract string GetStringRepresentation();

        // returns whether the goal is compleated (some wont ever be compleated 
        public abstract bool IsCompleted();

        //simple getters for ui or debugging
        public string GetName() => _name;
        public string GetDescription() => _description;
        public int GetPoints() => _points;

        // Setters for editing
        public void SetName(string name) => _name = name;
        public void SetDescription(string description) => _description = description;

    }
}