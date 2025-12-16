using System;
using System.Runtime.InteropServices;

namespace FinalProject.Commands
{
    /// <summary>
    /// SystemInfoCommand displays system information.
    /// Demonstrates inheritance and polymorphic command execution.
    /// Encapsulates system information gathering logic.
    /// </summary>
    public class SystemInfoCommand : Command
    {
        public SystemInfoCommand() : base("sysinfo", "Display system information") { }

        /// <summary>
        /// Executes system info command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                string osDesc = RuntimeInformation.OSDescription;
                string arch = RuntimeInformation.OSArchitecture.ToString();
                string runtime = RuntimeInformation.FrameworkDescription;

                string output = $@"💻 Operating System:  {osDesc}
🏗️  Architecture:      {arch}
⚙️  Runtime:           {runtime}
📊 Processor Count:   {Environment.ProcessorCount}
🖥️  Computer Name:    {Environment.MachineName}
👤 Username:          {Environment.UserName}
🕐 Current Time:      {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                return CommandResult.SuccessResult(FormatOutput("SYSTEM INFORMATION", output));
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Failed to retrieve system info: {ex.Message}");
            }
        }
    }
}