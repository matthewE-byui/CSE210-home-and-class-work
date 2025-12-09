using System;
using System.Collections.Generic;
using FinalProject.Commands;

namespace FinalProject.Engine
{
    public class CommandEngine
    {
        private List<Command> _commands = new List<Command>();

        public CommandEngine()
        {
            // Register built-in commands
            _commands.Add(new TimeCommand());
            _commands.Add(new WeatherCommand());
            _commands.Add(new ExitCommand());
            _commands.Add(new MathCommand());
            _commands.Add(new CreateFileCommand());
            _commands.Add(new OpenAppCommand());
            _commands.Add(new SystemInfoCommand());
            _commands.Add(new HelpCommand());
            _commands.Add(new MacroCommand());
            _commands.Add(new AutomationCommand());
            _commands.Add(new LookupCommand());
        }

        public string Process(string input)
        {
            input = input.ToLower().Trim();

            // Resolve aliases first
            input = AliasCommand.ResolveAlias(input);

            // Check for exact command matches
            foreach (var cmd in _commands)
            {
                if (input == cmd.Name || input.StartsWith(cmd.Name + " "))
                {
                    return cmd.Execute(input);
                }
            }

            return "❌ Unknown command. Type 'help' to see available commands.";
        }
    }
}
