using System;

namespace FinalProject.Commands
{
    public class TimeCommand : Command
    {
        public TimeCommand() : base("time") { }

        public override string Execute(string input)
        {
            DateTime now = DateTime.Now;
            string result = $@"
╔════════════════════════════════════════╗
║         CURRENT DATE & TIME            ║
╚════════════════════════════════════════╝

📅 Date:     {now:dddd, MMMM dd, yyyy}
🕐 Time:     {now:HH:mm:ss}
⏱️  Seconds:   {now.Second}
📍 TimeZone: {TimeZoneInfo.Local.StandardName}
";

            return result;
        }
    }
}
