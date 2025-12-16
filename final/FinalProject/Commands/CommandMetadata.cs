namespace FinalProject.Commands
{
    /// <summary>
    /// Encapsulates metadata about a command.
    /// Demonstrates encapsulation of command information.
    /// Allows decoupling of command registry from implementation.
    /// </summary>
    public class CommandMetadata
    {
        public string Name { get; }
        public string Description { get; }
        public string Usage { get; }
        public string Category { get; }
        public string[] Aliases { get; }

        public CommandMetadata(string name, string description, string usage, string category, string[] aliases = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Usage = usage ?? string.Empty;
            Category = category ?? "General";
            Aliases = aliases ?? System.Array.Empty<string>();
        }
    }
}
