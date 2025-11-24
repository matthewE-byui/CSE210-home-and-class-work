// using System;

// class Program
// {
//     // Function 1: DisplayWelcome
//     static void DisplayWelcome()
//     {
//         Console.WriteLine("Welcome to the program!");
//     }

//     // Function 2: PromptUserName
//     static string PromptUserName()
//     {
//         Console.Write("Please enter your name: ");
//         string name = Console.ReadLine();
//         return name;
//     }

//     // Function 3: PromptUserNumber
//     static int PromptUserNumber()
//     {
//         Console.Write("Please enter your favorite number: ");
//         int number = int.Parse(Console.ReadLine());
//         return number;
//     }

//     // Function 4: PromptUserBirthYear (using out)
//     static void PromptUserBirthYear(out int birthYear)
//     {
//         Console.Write("Please enter the year you were born: ");
//         birthYear = int.Parse(Console.ReadLine());
//     }

//     // Function 5: SquareNumber
//     static int SquareNumber(int number)
//     {
//         int square = number * number;
//         return square;
//     }

//     // Function 6: DisplayResult
//     static void DisplayResult(string name, int square, int birthYear)
//     {
//         Console.WriteLine($"{name}, the square of your number is {square}");
//         int currentYear = DateTime.Now.Year;
//         int age = currentYear - birthYear;
//         Console.WriteLine($"{name}, you will turn {age} this year.");
//     }

//     // Main Program
//     static void Main(string[] args)
//     {
//         DisplayWelcome();

//         string userName = PromptUserName();
//         int favoriteNumber = PromptUserNumber();
//         PromptUserBirthYear(out int birthYear);

//         int squaredNumber = SquareNumber(favoriteNumber);

//         DisplayResult(userName, squaredNumber, birthYear);
//     }
// }
