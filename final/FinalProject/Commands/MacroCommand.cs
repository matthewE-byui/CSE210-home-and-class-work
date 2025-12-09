using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Commands
{
    public class MacroCommand : Command
    {
        private static Dictionary<string, string> _macros = new Dictionary<string, string>();

        public MacroCommand() : base("macro") { }

        public override string Execute(string input)
        {
            try
            {
                string[] parts = input.Replace("macro", "").Trim().Split(new[] { ' ' }, 2);

                if (parts.Length == 0)
                    return "Usage: macro <list|run|save> [name] [commands]";

                string action = parts[0].ToLower();

                switch (action)
                {
                    case "list":
                        return ListMacros();

                    case "run":
                        if (parts.Length < 2)
                            return "Usage: macro run <macro_name>";
                        return RunMacro(parts[1].Trim());

                    case "save":
                        if (parts.Length < 2)
                            return "Usage: macro save <name> <command1>; <command2>; ...";
                        return SaveMacro(input);

                    default:
                        return "Unknown macro action. Use: list, run, or save";
                }
            }
            catch (Exception ex)
            {
                return $"Macro error: {ex.Message}";
            }
        }

        private string ListMacros()
        {
            if (_macros.Count == 0)
                return "No macros saved yet. Create one with: macro save <name> <commands>";

            string result = "╔════════════════════════════════════════╗\n";
            result += "║          SAVED MACROS                   ║\n";
            result += "╚════════════════════════════════════════╝\n\n";

            foreach (var macro in _macros)
            {
                result += $"📌 {macro.Key}\n";
                result += $"   Commands: {macro.Value}\n\n";
            }

            return result;
        }

        private string SaveMacro(string input)
        {
            // Format: macro save <name> <commands>
            string[] parts = input.Replace("macro save", "").Trim().Split(new[] { ' ' }, 2);
            
            if (parts.Length < 2)
                return "Usage: macro save <name> <command1>; <command2>; ...";

            string macroName = parts[0].Trim();
            string commands = parts[1].Trim();

            _macros[macroName] = commands;
            return $"✓ Macro '{macroName}' saved successfully!\n  Run it with: macro run {macroName}";
        }

        private string RunMacro(string macroName)
        {
            if (!_macros.ContainsKey(macroName))
                return $"Macro '{macroName}' not found. Use 'macro list' to see available macros.";

            string commands = _macros[macroName];
            string result = $"▶ Executing macro: {macroName}\n\n";

            // Split by semicolon for multiple commands
            var commandList = commands.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commandList)
            {
                result += $"[Running: {cmd.Trim()}]\n";
                // Note: In real implementation, would pass to CommandEngine
                result += "---\n";
            }

            return result;
        }
    }
}
