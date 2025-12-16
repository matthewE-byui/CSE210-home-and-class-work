using System;
using System.Data;

namespace FinalProject.Commands
{
    /// <summary>
    /// MathCommand evaluates mathematical expressions.
    /// Demonstrates inheritance and polymorphic Execute implementation.
    /// </summary>
    public class MathCommand : Command
    {
        public MathCommand() : base("math", "Evaluate mathematical expressions") { }

        /// <summary>
        /// Executes math evaluation.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling encapsulation.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            try
            {
                // Extract the expression after "math" command
                string expression = ExtractParameter(input);

                if (string.IsNullOrWhiteSpace(expression))
                    return CommandResult.ErrorResult("Usage: math <expression>\nExample: math 5+3*2");

                var table = new DataTable();
                var result = table.Compute(expression, "");

                string output = $"📝 Expression: {expression}\n✓  Result:     {result}";
                return CommandResult.SuccessResult(FormatOutput("MATH CALCULATOR", output));
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Invalid math expression: {ex.Message}\nExample: math 5+3*2");
            }
        }
    }
}
