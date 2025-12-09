using System;
using System.IO;

namespace FinalProject.Commands
{
    public class CreateFileCommand : Command
    {
        public CreateFileCommand() : base("createfile") {}

        public override string Execute(string input)
        {
            // Example: createfile notes.txt Hello world
            string[] parts = input.Split(" ", 3);

            if (parts.Length < 3)
                return "Usage: createfile <filename> <content>";

            string filename = parts[1];
            string text = parts[2];

            File.WriteAllText(filename, text);

            return $"Created file '{filename}' with content.";
        }
    }
}
