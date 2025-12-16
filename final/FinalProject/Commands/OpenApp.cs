using System;
using System.Diagnostics;

namespace FinalProject.Commands
{
    /// <summary>
    /// OpenAppCommand launches applications.
    /// Demonstrates inheritance and polymorphic command execution.
    /// Encapsulates process execution logic.
    /// </summary>
    public class OpenAppCommand : Command
    {
        public OpenAppCommand() : base("open", "Open an application") { }

        /// <summary>
        /// Executes open app command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            string program = ExtractParameter(input).ToLower();

            if (string.IsNullOrWhiteSpace(program))
                return CommandResult.ErrorResult("Usage: open <app>\nSupported: notepad, calc, edge");

            try
            {
                string exeName = program switch
                {
                    "notepad" => "notepad.exe",
                    "calculator" or "calc" => "calc.exe",
                    "edge" => "msedge.exe",
                    _ => null
                };

                if (exeName == null)
                    return CommandResult.ErrorResult("Unknown app. Try: notepad, calc, edge");

                Process.Start(exeName);
                return CommandResult.SuccessResult($"✓ Opening {program}...");
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Failed to open {program}: {ex.Message}");
            }
        }
    }
}