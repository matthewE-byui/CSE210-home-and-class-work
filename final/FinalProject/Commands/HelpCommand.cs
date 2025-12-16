using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Commands
{
    /// <summary>
    /// HelpCommand displays available commands dynamically from the registry.
    /// Demonstrates decoupling: no hard-coded command information.
    /// Works with ICommandExecutor to access command registry.
    /// Shows proper use of dependency injection pattern.
    /// </summary>
    public class HelpCommand : Command
    {
        // Dependency: Will be injected by the engine when Execute is called
        private ICommandExecutor _executor;

        public HelpCommand() : base("help", "Display available commands and usage information") { }

        /// <summary>
        /// Sets the executor for accessing the registry.
        /// Demonstrates dependency injection without constructor pollution.
        /// </summary>
        public void SetExecutor(ICommandExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Executes help command with dynamically generated content.
        /// No hard-coded command list - reads from registry.
        /// If executor is not set, falls back to basic help.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            if (_executor == null)
                return CommandResult.SuccessResult(FormatOutput("AVAILABLE COMMANDS", GetBasicHelp()));

            string helpText = GenerateHelpFromRegistry(_executor.GetRegistry());
            return CommandResult.SuccessResult(FormatOutput("AVAILABLE COMMANDS IN JARVIS", helpText));
        }

        /// <summary>
        /// Dynamically generates help text from the command registry.
        /// Groups commands by category and displays them.
        /// This ensures help is always in sync with registered commands.
        /// </summary>
        private string GenerateHelpFromRegistry(CommandRegistry registry)
        {
            var output = "";
            
            // Group commands by category
            var categoriesGroup = registry.GetCommandsByCategory();

            foreach (var category in categoriesGroup)
            {
                output += $"\n{category.Key}\n";
                
                foreach (var commandKvp in category)
                {
                    var meta = commandKvp.Value;
                    output += $"    • {meta.Name,-15} - {meta.Description}\n";
                    
                    if (meta.Aliases.Length > 0)
                    {
                        output += $"      Aliases: {string.Join(", ", meta.Aliases)}\n";
                    }
                }
            }

            output += "\n════════════════════════════════════════════════════════\n";
            output += "💡 TIP: Try natural language! Commands accept aliases like:\n";
            output += "   'what time is it' instead of 'time'\n";
            output += "   'how is the weather' instead of 'weather'\n";

            return output;
        }

        /// <summary>
        /// Basic help text used when registry is unavailable.
        /// </summary>
        private string GetBasicHelp()
        {
            return @"Type 'help' to see commands grouped by category.
The system supports many natural language aliases!

Common commands:
  • time     - Display current date and time
  • weather  - Get weather information
  • math     - Calculate expressions
  • help     - Show this help menu
  • exit     - Exit the application";
        }
    }
}