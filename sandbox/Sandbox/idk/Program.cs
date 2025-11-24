// using System;

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("Hello from the outside!");
//         Console.Write("what is your favoirite car? ");
//         string car = Console.ReadLine();
//         Console.WriteLine($"Your favorite car is {car}\n");
//         Console.WriteLine("What other things do you like?");
//         string things = Console.ReadLine();
//         Console.WriteLine($"You like {things} too!\n");
//         Console.WriteLine("lets do some math!");
//         Console.Write("Enter a number: ");
//         string numb1 = Console.ReadLine();
//         Console.Write("Enter another number: ");
//         string numb2 = Console.ReadLine();
//         int number1 = int.Parse(numb1);
//         int number2 = int.Parse(numb2);
//         Console.WriteLine("Would you like to add, subtract, multiply or divide?");
//         string operation = Console.ReadLine();
//         int result = 0;
//         if (operation == "add")
//         {
//             result = number1 + number2;
//         }
//         else if (operation == "subtract")
//         {
//             result = number1 - number2;
//         }
//         else if (operation == "multiply")
//         {
//             result = number1 * number2;
//         }
//         else if (operation == "divide")
//         {
//             result = number1 / number2;
//         }
//         Console.WriteLine($"The result is {result}");
//         Console.WriteLine("Goodbye!");

//     }
// }