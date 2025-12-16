using System;

namespace FinalProject.Commands
{
    /// <summary>
    /// Interface defining the contract for all executable commands.
    /// Demonstrates abstraction through interface segregation.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the name of the command
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of what the command does
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes the command with the given input
        /// </summary>
        CommandResult Execute(string input);

        /// <summary>
        /// Determines if the given input matches this command
        /// </summary>
        bool Matches(string input);
    }
}
