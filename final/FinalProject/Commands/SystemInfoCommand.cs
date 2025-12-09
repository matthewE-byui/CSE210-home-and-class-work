using System;
using System.Runtime.InteropServices;

namespace FinalProject.Commands
{
    public class SystemInfoCommand : Command
    {
        public SystemInfoCommand() : base("sysinfo") {}

        public override string Execute(string input)
        {
            string osDesc = RuntimeInformation.OSDescription;
            string arch = RuntimeInformation.OSArchitecture.ToString();
            string runtime = RuntimeInformation.FrameworkDescription;

            string result = $@"
╔════════════════════════════════════════════════════════╗
║           SYSTEM INFORMATION                           ║
╚════════════════════════════════════════════════════════╝

💻 Operating System:  {osDesc}
🏗️  Architecture:      {arch}
⚙️  Runtime:           {runtime}
📊 Processor Count:   {Environment.ProcessorCount}
🖥️  Computer Name:    {Environment.MachineName}
👤 Username:          {Environment.UserName}
🕐 Current Time:      {DateTime.Now:yyyy-MM-dd HH:mm:ss}
";

            return result;
        }
    }
}
