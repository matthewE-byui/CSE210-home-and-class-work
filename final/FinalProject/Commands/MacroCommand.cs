using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Commands
{
    /// <summary>
    /// MacroCommand allows users to chain multiple commands together.
    /// Demonstrates dependency injection: receives executor to enable command chaining.
    /// Shows proper command interaction without tight coupling.
    /// </summary>
    public class MacroCommand : Command
    {
        // Encapsulation: Private static storage of macros
        private static Dictionary<string, string> _macros = new Dictionary<string, string>();
        // Dependency: Injected by engine to enable executing other commands
        private ICommandExecutor _executor;

        public MacroCommand() : base("macro", "Create and execute command chains (macros)") { }

        /// <summary>
        /// Sets the executor for running chained commands.
        /// Demonstrates dependency injection pattern.
        /// </summary>
        public void SetExecutor(ICommandExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Executes macro commands (list, run, save).
        /// Demonstrates polymorphism and proper command interactions.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                string[] parts = ExtractParameter(input).Split(new[] { ' ' }, 2);

                if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0]))
                    return CommandResult.ErrorResult("Usage: macro <list|run|save> [name] [commands]");

                string action = parts[0].ToLower();

                return action switch
                {
                    "list" => ListMacros(),
                    "run" => parts.Length < 2 
                        ? CommandResult.ErrorResult("Usage: macro run <macro_name>")
                        : RunMacro(parts[1].Trim()),
                    "save" => parts.Length < 2
                        ? CommandResult.ErrorResult("Usage: macro save <name> <command1>; <command2>; ...")
                        : SaveMacro(input),
                    _ => CommandResult.ErrorResult("Unknown macro action. Use: list, run, or save")
                };
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Macro error: {ex.Message}");
            }
        }

        private CommandResult ListMacros()
        {
            if (_macros.Count == 0)
                return CommandResult.SuccessResult(FormatOutput("SAVED MACROS", "No macros saved yet. Create one with: macro save <name> <commands>"));

            string output = "";
            foreach (var macro in _macros)
            {
                output += $"📌 {macro.Key}\n";
                output += $"   Commands: {macro.Value}\n\n";
            }

            return CommandResult.SuccessResult(FormatOutput("SAVED MACROS", output.TrimEnd()));
        }

        private CommandResult SaveMacro(string input)
        {
            string[] parts = input.Replace("macro save", "").Trim().Split(new[] { ' ' }, 2);
            
            if (parts.Length < 2)
                return CommandResult.ErrorResult("Usage: macro save <name> <command1>; <command2>; ...");

            string macroName = parts[0].Trim();
            string commands = parts[1].Trim();

            _macros[macroName] = commands;
            return CommandResult.SuccessResult($"✓ Macro '{macroName}' saved successfully!\n  Run it with: macro run {macroName}");
        }

        private CommandResult RunMacro(string macroName)
        {
            if (!_macros.ContainsKey(macroName))
                return CommandResult.ErrorResult($"Macro '{macroName}' not found. Use 'macro list' to see available macros.");

            string commands = _macros[macroName];
            string output = $"▶ Executing macro: {macroName}\n\n";

            var commandList = commands.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commandList)
            {
                string trimmedCmd = cmd.Trim();
                output += $"[Running: {trimmedCmd}]\n";
                
                // If executor is available, execute the chained command
                if (_executor != null)
                {
                    // Extract command name and input
                    string[] cmdParts = trimmedCmd.Split(new[] { ' ' }, 2);
                    string cmdName = cmdParts[0];
                    string cmdInput = cmdParts.Length > 1 ? trimmedCmd : cmdName;
                    
                    // Execute the command through the executor
                    CommandResult result = _executor.ExecuteCommand(cmdName, cmdInput);
                    output += result.ToString() + "\n";
                }
                
                output += "---\n";
            }

            return CommandResult.SuccessResult(output);
        }
    }
}