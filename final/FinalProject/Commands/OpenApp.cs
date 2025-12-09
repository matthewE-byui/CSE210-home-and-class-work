using System;
using System.Diagnostics;

namespace FinalProject.Commands
{
    public class OpenAppCommand : Command
    {
        public OpenAppCommand() : base("open") {}

        public override string Execute(string input)
        {
            // Example: open notepad
            string program = input.Replace("open", "").Trim().ToLower();

            try
            {
                if (program == "notepad")
                    Process.Start("notepad.exe");
                else if (program == "calculator" || program == "calc")
                    Process.Start("calc.exe");
                else if (program == "edge")
                    Process.Start("msedge.exe");
                else
                    return "Unknown app. Try: notepad, calc, edge.";

                return $"Opening {program}...";
            }
            catch
            {
                return "Failed to open program.";
            }
        }
    }
}
