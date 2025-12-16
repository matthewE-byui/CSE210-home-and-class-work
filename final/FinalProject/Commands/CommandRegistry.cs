using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Commands
{
    /// <summary>
    /// Central registry of all available commands and their metadata.
    /// Demonstrates encapsulation and reduces coupling between commands.
    /// Single source of truth for command information (no hard-coding in HelpCommand).
    /// </summary>
    public class CommandRegistry
    {
        private readonly Dictionary<string, CommandMetadata> _metadata = new Dictionary<string, CommandMetadata>();

        /// <summary>
        /// Registers command metadata.
        /// Encapsulates how command information is stored.
        /// </summary>
        public void Register(string commandName, CommandMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentException("Command name cannot be null or empty", nameof(commandName));

            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            _metadata[commandName.ToLower()] = metadata;
        }

        /// <summary>
        /// Retrieves metadata for a command.
        /// </summary>
        public CommandMetadata GetMetadata(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                return null;

            return _metadata.TryGetValue(commandName.ToLower(), out var meta) ? meta : null;
        }

        /// <summary>
        /// Gets all registered commands grouped by category.
        /// Allows dynamic generation of help text without hard-coding.
        /// </summary>
        public IEnumerable<IGrouping<string, KeyValuePair<string, CommandMetadata>>> GetCommandsByCategory()
        {
            return _metadata
                .GroupBy(kvp => kvp.Value.Category)
                .OrderBy(g => g.Key);
        }

        /// <summary>
        /// Gets all registered command names.
        /// </summary>
        public IEnumerable<string> GetAllCommandNames()
        {
            return _metadata.Keys;
        }

        /// <summary>
        /// Checks if a command is registered.
        /// </summary>
        public bool IsCommandRegistered(string commandName)
        {
            return !string.IsNullOrWhiteSpace(commandName) && 
                   _metadata.ContainsKey(commandName.ToLower());
        }
    }
}
