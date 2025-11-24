// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         string playAgain = "yes";

//         while (playAgain == "yes")
//         {
//             Random randomGenerator = new Random();
//             int magicNumber = randomGenerator.Next(1, 101);
//             int guess = -1;
//             int guessCount = 0;
//             Console.WriteLine("I'm thinking of a number between 1 and 100.");
//             while (guess != magicNumber)
//             {
//                 Console.Write("What is your guess? ");
//                 guess = int.Parse(Console.ReadLine());
//                 guessCount++;

//                 if (guess < magicNumber)
//                 {
//                     Console.Write("higher");
//                     Console.WriteLine();
//                 }
//                 else if (guess > magicNumber)
//                 {
//                     Console.Write("Lower");
//                     Console.WriteLine();

//                 }
//                 else
//                 {
//                     Console.Write("You guessed it!");
//                     Console.WriteLine();

//                 }
//             }   
//         }
//     }
// }

// practice is done below


// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         string playAgain = "yes";

//         while (playAgain == "yes")
//         {
//             Random randomGenerator = new Random();
//             int magicNumber = randomGenerator.Next(1, 101);
//             int guess = -1;
//             int guessCount = 0;
//             Console.WriteLine("I am thinking of a number between 1 and 100.");
//             while (guess != magicNumber)
//             {
//                 Console.Write("what is your guess? ");
//                 guess = int.Parse(Console.ReadLine());
//                 guessCount++;

//                 if (guess < magicNumber)
//                 {
//                     Console.Write("Higher");
//                     Console.WriteLine();
//                 }
//                 else if (guess > magicNumber)
//                 {
//                     Console.Write("Lower");
//                     Console.WriteLine();
//                 }
//                 else
//                 {
//                     Console.Write("You guessed it!");
//                     Console.WriteLine();
//                 }

//             }

//         }
//     }
    
// }