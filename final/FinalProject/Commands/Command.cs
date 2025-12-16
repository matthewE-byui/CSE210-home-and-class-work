namespace FinalProject.Commands
{
    /// <summary>
    /// Abstract base class for all commands.
    /// Demonstrates abstraction, encapsulation, and polymorphism.
    /// - Abstraction: Defines common interface for all commands
    /// - Encapsulation: Private backing field with protected property
    /// - Polymorphism: Abstract Execute method overridden by subclasses
    /// </summary>
    public abstract class Command : ICommand
    {
        // Encapsulation: Private backing field, public read-only property
        private readonly string _name;
        private readonly string _description;

        public string Name => _name;
        public string Description => _description;

        /// <summary>
        /// Protected constructor for subclasses
        /// Encapsulates initialization logic
        /// </summary>
        protected Command(string name, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Command name cannot be null or empty", nameof(name));

            _name = name.ToLower().Trim();
            _description = description;
        }

        /// <summary>
        /// Abstract method demonstrating polymorphism.
        /// Each subclass provides its own implementation.
        /// </summary>
        public abstract CommandResult Execute(string input);

        /// <summary>
        /// Virtual method for matching input to this command.
        /// Can be overridden by subclasses for custom matching logic.
        /// Demonstrates polymorphism through virtual methods.
        /// </summary>
        public virtual bool Matches(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string lowerInput = input.ToLower().Trim();
            return lowerInput == _name || lowerInput.StartsWith(_name + " ");
        }

        /// <summary>
        /// Protected helper method for subclasses to validate and extract parameters
        /// Demonstrates encapsulation of common functionality
        /// </summary>
        protected string ExtractParameter(string input, int startIndex = -1)
        {
            if (startIndex == -1)
                startIndex = _name.Length;

            return input.Substring(startIndex).Trim();
        }

        /// <summary>
        /// Protected helper for formatting output consistently
        /// Demonstrates encapsulation of presentation logic
        /// </summary>
        protected string FormatOutput(string title, string content)
        {
            return $@"
╔════════════════════════════════════════╗
║   {title.PadRight(38)}║
╚════════════════════════════════════════╝

{content}
";
        }
    }
}