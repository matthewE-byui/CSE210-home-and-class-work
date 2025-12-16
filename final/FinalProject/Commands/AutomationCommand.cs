using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinalProject.Commands
{
    /// <summary>
    /// AutomationCommand manages automated tasks.
    /// Demonstrates inheritance, polymorphism, and encapsulation:
    /// - Inheritance: Inherits from Command
    /// - Polymorphism: Overrides Execute method
    /// - Encapsulation: Private helper classes and static fields for task management
    /// </summary>
    public class AutomationCommand : Command
    {
        // Encapsulation: Private static storage for tasks
        private static Dictionary<string, AutomatedTask> _tasks = new Dictionary<string, AutomatedTask>();
        private static List<AutomationScheduler> _schedulers = new List<AutomationScheduler>();

        public AutomationCommand() : base("automate", "Create and execute automated tasks") { }

        /// <summary>
        /// Executes automation commands (list, add, run, info).
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                string[] parts = ExtractParameter(input).Split(new[] { ' ' }, 2);

                if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0]))
                    return CommandResult.ErrorResult("Usage: automate <list|add|run|info>");

                string action = parts[0].ToLower();

                return action switch
                {
                    "list" => ListTasks(),
                    "add" => parts.Length < 2
                        ? CommandResult.ErrorResult("Usage: automate add <name> <description>")
                        : AddTask(parts[1]),
                    "run" => parts.Length < 2
                        ? CommandResult.ErrorResult("Usage: automate run <task_name>")
                        : RunTask(parts[1].Trim()),
                    "info" => parts.Length < 2
                        ? CommandResult.ErrorResult("Usage: automate info <task_name>")
                        : GetTaskInfo(parts[1].Trim()),
                    _ => CommandResult.ErrorResult("Unknown automation action. Use: list, add, run, or info")
                };
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Automation error: {ex.Message}");
            }
        }

        private CommandResult ListTasks()
        {
            if (_tasks.Count == 0)
                return CommandResult.SuccessResult(FormatOutput("AUTOMATED TASKS", "No automated tasks created yet. Create one with: automate add <name> <description>"));

            string output = "";
            int index = 1;
            foreach (var task in _tasks)
            {
                output += $"{index}. 🤖 {task.Key}\n";
                output += $"   Status: {(task.Value.IsEnabled ? "✓ Enabled" : "✗ Disabled")}\n";
                output += $"   Created: {task.Value.CreatedTime}\n\n";
                index++;
            }

            return CommandResult.SuccessResult(FormatOutput("AUTOMATED TASKS", output.TrimEnd()));
        }

        private CommandResult AddTask(string taskData)
        {
            string[] parts = taskData.Split(new[] { ' ' }, 2);
            if (parts.Length < 2)
                return CommandResult.ErrorResult("Usage: automate add <name> <description>");

            string taskName = parts[0].Trim();
            string description = parts[1].Trim();

            _tasks[taskName] = new AutomatedTask
            {
                Name = taskName,
                Description = description,
                CreatedTime = DateTime.Now,
                IsEnabled = true
            };

            return CommandResult.SuccessResult($"✓ Automated task '{taskName}' created successfully!\n  Description: {description}");
        }

        private CommandResult RunTask(string taskName)
        {
            if (!_tasks.ContainsKey(taskName))
                return CommandResult.ErrorResult($"Task '{taskName}' not found. Use 'automate list' to see available tasks.");

            var task = _tasks[taskName];
            if (!task.IsEnabled)
                return CommandResult.ErrorResult($"Task '{taskName}' is disabled. Cannot run.");

            task.LastRun = DateTime.Now;
            task.RunCount++;

            string output = $@"▶ Running automation task: {taskName}
📝 Description: {task.Description}
⏱️  Started at: {task.LastRun:HH:mm:ss}

[Task execution simulated - running automation...]
[Checking dependencies...]
[Executing main process...]
[Collecting results...]

✓ Task completed successfully!
📊 Total runs: {task.RunCount}";

            return CommandResult.SuccessResult(FormatOutput("AUTOMATION EXECUTION", output));
        }

        private CommandResult GetTaskInfo(string taskName)
        {
            if (!_tasks.ContainsKey(taskName))
                return CommandResult.ErrorResult($"Task '{taskName}' not found.");

            var task = _tasks[taskName];
            string output = $@"📌 Name:        {task.Name}
📝 Description: {task.Description}
✓  Status:      {(task.IsEnabled ? "Enabled" : "Disabled")}
📅 Created:     {task.CreatedTime:yyyy-MM-dd HH:mm:ss}
🕐 Last Run:    {(task.LastRun.HasValue ? task.LastRun.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Never")}
🔢 Total Runs:  {task.RunCount}";

            return CommandResult.SuccessResult(FormatOutput("TASK INFORMATION", output));
        }

        /// <summary>
        /// Encapsulated private class representing an automated task.
        /// </summary>
        private class AutomatedTask
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime? LastRun { get; set; }
            public bool IsEnabled { get; set; }
            public int RunCount { get; set; }
        }

        /// <summary>
        /// Encapsulated private class for task scheduling.
        /// </summary>
        private class AutomationScheduler
        {
            public string TaskName { get; set; }
            public DateTime ScheduledTime { get; set; }
        }
    }
}