namespace FinalProject.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand() : base("exit") { }

        public override string Execute(string input)
        {
            return "EXIT";
        }
    }
}
