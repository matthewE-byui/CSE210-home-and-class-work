namespace FinalProject.Commands
{
    /// <summary>
    /// ExitCommand terminates the application.
    /// Demonstrates polymorphism through command inheritance.
    /// </summary>
    public class ExitCommand : Command
    {
        public ExitCommand() : base("exit", "Exit the application") { }

        /// <summary>
        /// Special exit command that signals termination to the engine.
        /// Demonstrates polymorphic behavior for special command handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            // Return a special marker for exit
            return CommandResult.SuccessResult("EXIT");
        }
    }
}