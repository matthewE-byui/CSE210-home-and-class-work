using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinalProject.Commands
{
    public class OpenAppCommand : Command
    {
        // Encapsulated app registry
        private readonly Dictionary<string, string> _apps;

        public OpenAppCommand()
            : base("open", "Open an application")
        {
            _apps = new Dictionary<string, string>
            {
                { "notepad", "notepad.exe" },
                { "calc", "calc.exe" },
                { "calculator", "calc.exe" },
                { "edge", "msedge.exe" },
                { "chrome", "chrome.exe" },
                { "explorer", "explorer.exe" },
                { "cmd", "cmd.exe" },
                { "powershell", "powershell.exe" },
                { "spotify", "spotify.exe" },
                { "word", "winword.exe" },
                { "excel", "excel.exe" },
                { "powerpoint", "powerpnt.exe" },
                { "teams", "Teams.exe" },
                { "trailmakers", "Trailmakers.exe" }
            };
        }

        public override CommandResult Execute(string input)
        {
            string program = ExtractParameter(input).ToLower();

            if (string.IsNullOrWhiteSpace(program))
                return CommandResult.ErrorResult(
                    "Usage: open <app>\nExample: open notepad");

            try
            {
                if (!_apps.TryGetValue(program, out string exeName))
                    return CommandResult.ErrorResult(
                        $"Unknown app '{program}'. Try: {string.Join(", ", _apps.Keys)}");

                Process.Start(new ProcessStartInfo
                {
                    FileName = exeName,
                    UseShellExecute = true
                });

                return CommandResult.SuccessResult($"✓ Opening {program}...");
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Failed to open {program}: {ex.Message}");
            }
        }
    }
}
