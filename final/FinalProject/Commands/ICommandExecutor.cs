namespace FinalProject.Commands
{
    /// <summary>
    /// Interface for command execution context.
    /// Allows commands to interact with the engine without tight coupling.
    /// Demonstrates dependency injection pattern for command interactions.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes a command by name with given input.
        /// Allows MacroCommand and AutomationCommand to chain command execution.
        /// </summary>
        CommandResult ExecuteCommand(string commandName, string input);

        /// <summary>
        /// Gets the command registry for metadata lookup.
        /// </summary>
        CommandRegistry GetRegistry();
    }
}
