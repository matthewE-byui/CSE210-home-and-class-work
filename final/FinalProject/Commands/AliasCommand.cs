using System;
using System.Collections.Generic;

namespace FinalProject.Commands
{
    public class AliasCommand : Command
    {
        private static Dictionary<string, string> _aliases = new Dictionary<string, string>()
        {
            // Time-related
            { "what time is it", "time" },
            { "tell me the time", "time" },
            { "current time", "time" },
            
            // Weather-related
            { "how is the weather", "weather" },
            { "what's the weather", "weather" },
            { "weather report", "weather" },
            
            // System info
            { "system info", "sysinfo" },
            { "cpu", "sysinfo" },
            { "system status", "sysinfo" },
            
            // Help
            { "?", "help" },
            { "commands", "help" },
            { "show commands", "help" },
            
            // Search/Lookup - exact matches
            { "lookup", "lookup" },
            { "look up", "lookup" },
            { "search", "lookup" },
            { "search for", "lookup" },
            { "google", "lookup" },
            { "google for", "lookup" },
            { "find", "lookup" },
            { "find me", "lookup" },
            { "what is", "lookup" },
            { "what are", "lookup" },
            { "who is", "lookup" },
            { "tell me about", "lookup" },
            { "explain", "lookup" },
            { "definition of", "lookup" },
            { "how to", "lookup" },
            { "how do i", "lookup" },
            { "how does", "lookup" },
            
            // Exit
            { "quit", "exit" }
        };

        public AliasCommand() : base("__alias__") { }

        public override string Execute(string input)
        {
            // This command is typically used internally
            return "Alias command should not be called directly.";
        }

        public static string ResolveAlias(string input)
        {
            string lowerInput = input.ToLower().Trim();

            // Check for exact match first
            if (_aliases.ContainsKey(lowerInput))
            {
                return _aliases[lowerInput];
            }

            // Check for prefix match (for search queries like "what is python" -> "lookup python")
            foreach (var kvp in _aliases)
            {
                if (lowerInput.StartsWith(kvp.Key + " ") && kvp.Value == "lookup")
                {
                    // Extract the search term after the alias prefix
                    string searchTerm = lowerInput.Substring(kvp.Key.Length).Trim();
                    return $"lookup {searchTerm}";
                }
            }

            return input;
        }

        public static void AddAlias(string alias, string command)
        {
            _aliases[alias.ToLower()] = command.ToLower();
        }

        public static void ListAliases()
        {
            Console.WriteLine("Available Command Aliases:");
            foreach (var kvp in _aliases)
            {
                Console.WriteLine($"  '{kvp.Key}' → {kvp.Value}");
            }
        }
    }
}
