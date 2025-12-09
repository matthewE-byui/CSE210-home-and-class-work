using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FinalProject.Commands
{
    public class LookupCommand : Command
    {
        public LookupCommand() : base("lookup") { }

        public override string Execute(string input)
        {
            try
            {
                // input example: "lookup C# async await"
                string query = input.Replace("lookup", "").Trim();

                if (string.IsNullOrWhiteSpace(query))
                    return "Usage: lookup <search_term>\nExample: lookup C# async await";

                // URL encode the query for Google search
                string encodedQuery = Uri.EscapeDataString(query);
                string googleUrl = $"https://www.google.com/search?q={encodedQuery}";

                // Open in default browser
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
                    // Fallback for Linux/Mac or if UseShellExecute fails
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

                string result = $@"
╔════════════════════════════════════════╗
║         GOOGLE SEARCH LOOKUP           ║
╚════════════════════════════════════════╝

🔍 Query:    {query}
🌐 URL:      {googleUrl}
✓  Opening in default browser...
";

                return result;
            }
            catch (Exception ex)
            {
                return $"❌ Lookup error: {ex.Message}";
            }
        }
    }
}
