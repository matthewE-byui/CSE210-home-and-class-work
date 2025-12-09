using System;
using System.Data;

namespace FinalProject.Commands
{
    public class MathCommand : Command
    {
        public MathCommand() : base("math") {}

        public override string Execute(string input)
        {
            try
            {
                // input example: "math 5+9*2"
                string expression = input.Replace("math", "").Trim();

                if (string.IsNullOrWhiteSpace(expression))
                    return "Usage: math <expression>\nExample: math 5+3*2";

                var table = new DataTable();
                var result = table.Compute(expression, "");

                string output = $@"
╔════════════════════════════════════════╗
║            MATH CALCULATOR             ║
╚════════════════════════════════════════╝

📝 Expression: {expression}
✓  Result:     {result}
";

                return output;
            }
            catch (Exception ex)
            {
                return $"❌ Invalid math expression: {ex.Message}\nExample: math 5+3*2";
            }
        }
    }
}
