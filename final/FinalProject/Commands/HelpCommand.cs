using System;
using System.Collections.Generic;

namespace FinalProject.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help") { }

        public override string Execute(string input)
        {
            string help = @"
╔════════════════════════════════════════════════════════╗
║           AVAILABLE COMMANDS IN JARVIS                 ║
╚════════════════════════════════════════════════════════╝

📅  TIME
    • time                    - Display current date and time
    • 'what time is it'       - Alternative way to ask the time

🌤️   WEATHER
    • weather <city>          - Get current weather information
    • 'how is the weather'    - Alternative way to ask
    • Example: 'weather rexburg'

📊 SYSTEM INFO
    • sysinfo                 - Display system information
    • 'system info'           - Alternative way to ask
    • 'cpu'                   - Quick CPU/system check

🔢 MATH
    • math <expression>       - Calculate math expressions
    • Examples: 'math 5+3*2', 'math 100/5', 'math 2^8'

📄 FILE OPERATIONS
    • createfile <name>       - Create a new file
    • Examples: 'createfile test.txt', 'createfile data.csv'

🚀 APPLICATIONS
    • open <app_name>         - Open an application
    • Examples: 'open notepad', 'open calc', 'open explorer'

🔍 GOOGLE LOOKUP
    • lookup <query>          - Search Google for information
    • Natural language search prompts (all do the same thing):
      search, search for, google, google for, find, find me
      what is, what are, who is, tell me about, explain
      definition of, how to, how do i, how does
    • Examples: 'what is C# delegates', 'how to use Python decorators'

⚙️  MACROS (Command Chaining)
    • macro save <name> <cmd1>; <cmd2>   - Create command chain
    • macro run <name>        - Execute a saved macro
    • macro list              - List all saved macros
    • Example: 'macro save startup sysinfo; time'

🤖 AUTOMATION (Task Automation)
    • automate list           - List all automated tasks
    • automate add <name> <desc> - Create a new automation task
    • automate run <name>     - Execute an automated task
    • automate info <name>    - Get task information

🆘 GENERAL
    • help                    - Show this help menu
    • ?, commands             - Alternative ways to get help
    • exit, quit              - Exit the application

════════════════════════════════════════════════════════

🎯 SMART ALIASES (Natural Language)
    Try natural language alternatives like:
    • 'what time is it' instead of 'time'
    • 'how is the weather' instead of 'weather'
    • 'system info' instead of 'sysinfo'
    • 'show commands' instead of 'help'
    • Search queries: 'what is C#', 'how to code', 'find python docs'
      (and many more natural phrasing options)

════════════════════════════════════════════════════════";

            return help;
        }
    }
}
