using System;
using System.IO;

namespace FinalProject.Commands
{
    /// <summary>
    /// CreateFileCommand creates new files.
    /// Demonstrates inheritance and polymorphic command execution.
    /// Encapsulates file I/O operations.
    /// </summary>
    public class CreateFileCommand : Command
    {
        public CreateFileCommand() : base("createfile", "Create a new file with content") { }

        /// <summary>
        /// Executes create file command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            string[] parts = input.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
                return CommandResult.ErrorResult("Usage: createfile <filename> <content>\nExample: createfile notes.txt Hello world");

            try
            {
                string filename = parts[1];
                string content = parts[2];

                File.WriteAllText(filename, content);
                return CommandResult.SuccessResult($"✓ Created file '{filename}' with content.");
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Failed to create file: {ex.Message}");
            }
        }
    }
}