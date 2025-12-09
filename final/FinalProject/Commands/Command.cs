namespace FinalProject.Commands
{
    public abstract class Command
    {
        public string Name { get; private set; }

        protected Command(string name)
        {
            Name = name;
        }

        public abstract string Execute(string input);
    }
}
