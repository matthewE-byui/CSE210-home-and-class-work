using System;

namespace FinalProject.Commands
{
    /// <summary>
    /// Encapsulates the result of a command execution.
    /// Provides clear separation between success/failure states and output.
    /// Demonstrates encapsulation with private fields and public properties.
    /// </summary>
    public class CommandResult
    {
        private readonly string _output;
        private readonly bool _success;
        private readonly string _errorMessage;

        // Properties demonstrate read-only encapsulation
        public string Output => _output;
        public bool Success => _success;
        public string ErrorMessage => _errorMessage;

        private CommandResult(string output, bool success, string errorMessage)
        {
            _output = output ?? string.Empty;
            _success = success;
            _errorMessage = errorMessage ?? string.Empty;
        }

        /// <summary>
        /// Factory method for successful command execution
        /// Demonstrates encapsulation through factory pattern
        /// </summary>
        public static CommandResult SuccessResult(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
                throw new ArgumentException("Output cannot be null or empty for successful result", nameof(output));
            return new CommandResult(output, true, string.Empty);
        }

        /// <summary>
        /// Factory method for failed command execution
        /// </summary>
        public static CommandResult ErrorResult(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException("Error message cannot be null or empty", nameof(errorMessage));
            return new CommandResult(string.Empty, false, errorMessage);
        }

        public override string ToString() => Success ? Output : $"❌ Error: {ErrorMessage}";
    }
}
