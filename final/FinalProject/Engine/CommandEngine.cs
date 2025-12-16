using System;
using System.Collections.Generic;
using System.Linq;
using FinalProject.Commands;

namespace FinalProject.Engine
{
    /// <summary>
    /// CommandEngine orchestrates command execution.
    /// Implements ICommandExecutor to allow commands to interact without tight coupling.
    /// Demonstrates encapsulation, polymorphism, and dependency injection.
    /// - Encapsulation: Private fields, controlled access through public methods
    /// - Polymorphism: Works with base Command type, polymorphically calls Execute()
    /// - Loose coupling: Uses registry for metadata, provides executor interface for command interactions
    /// </summary>
    public class CommandEngine : ICommandExecutor
    {
        // Encapsulation: Private read-only list prevents external modification
        private readonly List<Command> _commands;
        // Registry holds metadata about commands - decouples HelpCommand from hard-coded text
        private readonly CommandRegistry _registry;

        public CommandEngine()
        {
            _commands = new List<Command>();
            _registry = new CommandRegistry();
            RegisterBuiltInCommands();
            InjectDependencies();
        }

        /// <summary>
        /// Injects the executor into commands that need it (HelpCommand, MacroCommand).
        /// Demonstrates dependency injection for decoupling.
        /// </summary>
        private void InjectDependencies()
        {
            var helpCmd = _commands.OfType<HelpCommand>().FirstOrDefault();
            if (helpCmd != null)
                helpCmd.SetExecutor(this);

            var macroCmd = _commands.OfType<MacroCommand>().FirstOrDefault();
            if (macroCmd != null)
                macroCmd.SetExecutor(this);
        }

        /// <summary>
        /// Registers all built-in commands with their metadata.
        /// Encapsulates command initialization and registration logic.
        /// Commands are registered with descriptions and usage information.
        /// </summary>
        private void RegisterBuiltInCommands()
        {
            // Time category
            RegisterCommand(new TimeCommand(), 
                new CommandMetadata("time", "Display current date and time", "time", "📅 Time",
                    new[] { "what time is it", "tell me the time" }));

            RegisterCommand(new WeatherCommand(),
                new CommandMetadata("weather", "Get current weather for a city", "weather <city>", "🌤️ Weather",
                    new[] { "how is the weather" }));

            RegisterCommand(new ExitCommand(),
                new CommandMetadata("exit", "Exit the application", "exit", "🆘 General",
                    new[] { "quit" }));

            RegisterCommand(new MathCommand(),
                new CommandMetadata("math", "Evaluate mathematical expressions", "math <expression>", "🔢 Math",
                    new[] { "calculate" }));

            RegisterCommand(new CreateFileCommand(),
                new CommandMetadata("createfile", "Create a new file", "createfile <name>", "📄 File Operations"));

            RegisterCommand(new OpenAppCommand(),
                new CommandMetadata("open", "Open an application", "open <app_name>", "🚀 Applications"));

            RegisterCommand(new SystemInfoCommand(),
                new CommandMetadata("sysinfo", "Display system information", "sysinfo", "📊 System Info",
                    new[] { "system info", "cpu" }));

            RegisterCommand(new HelpCommand(),
                new CommandMetadata("help", "Show available commands", "help", "🆘 General",
                    new[] { "?", "commands", "show commands" }));

            RegisterCommand(new MacroCommand(),
                new CommandMetadata("macro", "Create and execute command chains", "macro <list|run|save>", "⚙️ Macros"));

            RegisterCommand(new AutomationCommand(),
                new CommandMetadata("automate", "Automate task execution", "automate <list|add|run|info>", "🤖 Automation"));

            RegisterCommand(new LookupCommand(),
                new CommandMetadata("lookup", "Search for information", "lookup <query>", "🔍 Lookup",
                    new[] { "search", "google", "find" }));
        }

        /// <summary>
        /// Registers a command with its metadata.
        /// Encapsulates the registration of both command and metadata.
        /// </summary>
        private void RegisterCommand(Command command, CommandMetadata metadata)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (_commands.Any(c => c.Name == command.Name))
                throw new InvalidOperationException($"Command '{command.Name}' is already registered");

            _commands.Add(command);
            _registry.Register(command.Name, metadata);
        }

        /// <summary>
        /// Registers a new command dynamically.
        /// Encapsulates command registration logic.
        /// </summary>
        public void RegisterCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (_commands.Any(c => c.Name == command.Name))
                throw new InvalidOperationException($"Command '{command.Name}' is already registered");

            _commands.Add(command);
        }

        /// <summary>
        /// Processes user input and returns the result.
        /// Demonstrates polymorphism: all commands' Execute() methods are called through base class reference.
        /// </summary>
        public string Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "❌ Please enter a command. Type 'help' for available commands.";

            input = input.ToLower().Trim();

            // Resolve aliases first
            input = ResolveAliases(input);

            // Find and execute matching command
            Command matchedCommand = FindMatchingCommand(input);
            if (matchedCommand != null)
            {
                // Polymorphic call: each command's Execute implementation is called
                CommandResult result = matchedCommand.Execute(input);
                return result.ToString();
            }

            return "❌ Unknown command. Type 'help' to see available commands.";
        }

        /// <summary>
        /// Implements ICommandExecutor: Allows other commands to execute commands.
        /// Enables command interactions without tight coupling (e.g., MacroCommand chaining).
        /// </summary>
        public CommandResult ExecuteCommand(string commandName, string input)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                return CommandResult.ErrorResult("Command name cannot be empty");

            Command command = _commands.FirstOrDefault(c => c.Name == commandName.ToLower());
            if (command == null)
                return CommandResult.ErrorResult($"Unknown command: {commandName}");

            try
            {
                return command.Execute(input);
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Error executing command: {ex.Message}");
            }
        }

        /// <summary>
        /// Implements ICommandExecutor: Provides access to command registry.
        /// Allows commands like HelpCommand to dynamically generate help text.
        /// </summary>
        public CommandRegistry GetRegistry()
        {
            return _registry;
        }

        /// <summary>
        /// Finds the command that matches the given input.
        /// Encapsulates matching logic using polymorphic Matches() method.
        /// </summary>
        private Command FindMatchingCommand(string input)
        {
            return _commands.FirstOrDefault(cmd => cmd.Matches(input));
        }

        /// <summary>
        /// Resolves command aliases.
        /// Encapsulates alias resolution logic.
        /// </summary>
        private string ResolveAliases(string input)
        {
            return AliasCommand.ResolveAlias(input);
        }
    }
}