// using System;
// using System.Collections.Generic;

// namespace Prep4
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             List<int> numbers = new List<int>();

//             Console.WriteLine("Enter a list of numbers, type 0 when finished.");

//             int userNumber = -1;
//             while (userNumber != 0)
//             {
//                 Console.Write("Enter number: ");
//                 userNumber = int.Parse(Console.ReadLine());

//                 if (userNumber != 0)
//                 {
//                     numbers.Add(userNumber);
//                 }
//             }

//             int sum = 0;
//             foreach (int number in numbers)
//             {
//                 sum += number;
//             }

//             float average = ((float)sum) / numbers.Count;

//             int max = numbers[0];
//             foreach (int number in numbers)
//             {
//                 if (number > max)
//                 {
//                     max = number;
//                 }
//             }

//             // Find smallest positive number
//             int smallestPositive = int.MaxValue;
//             foreach (int number in numbers)
//             {
//                 if (number > 0 && number < smallestPositive)
//                 {
//                     smallestPositive = number;
//                 }
//             }

//             // Sort the list
//             numbers.Sort();

//             Console.WriteLine($"The sum is: {sum}");
//             Console.WriteLine($"The average is: {average}");
//             Console.WriteLine($"The largest number is: {max}");
//             Console.WriteLine($"The smallest positive number is: {smallestPositive}");

//             Console.WriteLine("The sorted list is:");
//             foreach (int number in numbers)
//             {
//                 Console.WriteLine(number);
//             }
//         }
//     }
// }
