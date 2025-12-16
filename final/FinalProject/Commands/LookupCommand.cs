using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FinalProject.Commands
{
    /// <summary>
    /// LookupCommand performs Google searches.
    /// Demonstrates inheritance and polymorphic command execution.
    /// Encapsulates browser launching and URL handling logic.
    /// </summary>
    public class LookupCommand : Command
    {
        public LookupCommand() : base("lookup", "Search Google for information") { }

        /// <summary>
        /// Executes lookup command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                string query = ExtractParameter(input);

                if (string.IsNullOrWhiteSpace(query))
                    return CommandResult.ErrorResult("Usage: lookup <search_term>\nExample: lookup C# async await");

                string encodedQuery = Uri.EscapeDataString(query);
                string googleUrl = $"https://www.google.com/search?q={encodedQuery}";

                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = googleUrl,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", googleUrl);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", googleUrl);
                    }
                    else
                    {
                        throw;
                    }
                }

                string output = $"🔍 Query:    {query}\n🌐 URL:      {googleUrl}\n✓  Opening in default browser...";
                return CommandResult.SuccessResult(FormatOutput("GOOGLE SEARCH LOOKUP", output));
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Lookup error: {ex.Message}");
            }
        }
    }
}