using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinalProject.Commands
{
    public class AutomationCommand : Command
    {
        private static Dictionary<string, AutomatedTask> _tasks = new Dictionary<string, AutomatedTask>();
        private static List<AutomationScheduler> _schedulers = new List<AutomationScheduler>();

        public AutomationCommand() : base("automate") { }

        public override string Execute(string input)
        {
            try
            {
                string[] parts = input.Replace("automate", "").Trim().Split(new[] { ' ' }, 2);

                if (parts.Length == 0)
                    return "Usage: automate <list|add|run|info>";

                string action = parts[0].ToLower();

                switch (action)
                {
                    case "list":
                        return ListTasks();

                    case "add":
                        if (parts.Length < 2)
                            return "Usage: automate add <name> <description>";
                        return AddTask(parts[1]);

                    case "run":
                        if (parts.Length < 2)
                            return "Usage: automate run <task_name>";
                        return RunTask(parts[1].Trim());

                    case "info":
                        if (parts.Length < 2)
                            return "Usage: automate info <task_name>";
                        return GetTaskInfo(parts[1].Trim());

                    default:
                        return "Unknown automation action. Use: list, add, run, or info";
                }
            }
            catch (Exception ex)
            {
                return $"Automation error: {ex.Message}";
            }
        }

        private string ListTasks()
        {
            if (_tasks.Count == 0)
                return "No automated tasks created yet. Create one with: automate add <name> <description>";

            string result = "╔════════════════════════════════════════════╗\n";
            result += "║        AUTOMATED TASKS                      ║\n";
            result += "╚════════════════════════════════════════════╝\n\n";

            int index = 1;
            foreach (var task in _tasks)
            {
                result += $"{index}. 🤖 {task.Key}\n";
                result += $"   Status: {(task.Value.IsEnabled ? "✓ Enabled" : "✗ Disabled")}\n";
                result += $"   Created: {task.Value.CreatedTime}\n\n";
                index++;
            }

            return result;
        }

        private string AddTask(string taskData)
        {
            string[] parts = taskData.Split(new[] { ' ' }, 2);
            if (parts.Length < 2)
                return "Usage: automate add <name> <description>";

            string taskName = parts[0].Trim();
            string description = parts[1].Trim();

            _tasks[taskName] = new AutomatedTask
            {
                Name = taskName,
                Description = description,
                CreatedTime = DateTime.Now,
                IsEnabled = true
            };

            return $"✓ Automated task '{taskName}' created successfully!\n  Description: {description}";
        }

        private string RunTask(string taskName)
        {
            if (!_tasks.ContainsKey(taskName))
                return $"Task '{taskName}' not found. Use 'automate list' to see available tasks.";

            var task = _tasks[taskName];
            if (!task.IsEnabled)
                return $"Task '{taskName}' is disabled. Cannot run.";

            task.LastRun = DateTime.Now;
            task.RunCount++;

            string result = $@"
▶ Running automation task: {taskName}
📝 Description: {task.Description}
⏱️  Started at: {task.LastRun:HH:mm:ss}

[Task execution simulated - running automation...]
[Checking dependencies...]
[Executing main process...]
[Collecting results...]

✓ Task completed successfully!
📊 Total runs: {task.RunCount}
";

            return result;
        }

        private string GetTaskInfo(string taskName)
        {
            if (!_tasks.ContainsKey(taskName))
                return $"Task '{taskName}' not found.";

            var task = _tasks[taskName];

            string result = $@"
╔════════════════════════════════════════════╗
║        TASK INFORMATION                     ║
╚════════════════════════════════════════════╝

📌 Name:        {task.Name}
📝 Description: {task.Description}
✓  Status:      {(task.IsEnabled ? "Enabled" : "Disabled")}
📅 Created:     {task.CreatedTime:yyyy-MM-dd HH:mm:ss}
🕐 Last Run:    {(task.LastRun.HasValue ? task.LastRun.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Never")}
🔢 Total Runs:  {task.RunCount}
";

            return result;
        }

        private class AutomatedTask
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime? LastRun { get; set; }
            public bool IsEnabled { get; set; }
            public int RunCount { get; set; }
        }

        private class AutomationScheduler
        {
            public string TaskName { get; set; }
            public DateTime ScheduledTime { get; set; }
        }
    }
}
