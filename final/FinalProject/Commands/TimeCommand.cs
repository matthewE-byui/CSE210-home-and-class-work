using System;

namespace FinalProject.Commands
{
    /// <summary>
    /// TimeCommand demonstrates inheritance and polymorphism.
    /// Inherits from Command and overrides Execute with specialized behavior.
    /// </summary>
    public class TimeCommand : Command
    {
        public TimeCommand() : base("time", "Display current date and time") { }

        /// <summary>
        /// Executes the time command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for encapsulation of success/failure.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                DateTime now = DateTime.Now;
                string output = $@"📅 Date:     {now:dddd, MMMM dd, yyyy}
🕐 Time:     {now:HH:mm:ss}
⏱️  Seconds:   {now.Second}
📍 TimeZone: {TimeZoneInfo.Local.StandardName}";

                return CommandResult.SuccessResult(FormatOutput("CURRENT DATE & TIME", output));
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Failed to retrieve time: {ex.Message}");
            }
        }
    }
}