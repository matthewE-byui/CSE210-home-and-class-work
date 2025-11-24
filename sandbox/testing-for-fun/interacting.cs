// using System;

// class Interacting
// {
//     // This is a helper method rather than an entry-point Main so it
//     // doesn't conflict with the project's existing Program/Main.
//     public static void RunInteractive()
//     {
//         Console.WriteLine("What is your name?");
//         // Console.ReadLine() may return null in nullable-enabled projects.
//         // Use the null-coalescing operator to get a safe non-null string.
//         string name = Console.ReadLine() ?? string.Empty;

//         Console.WriteLine($"Hello, {name}! Welcome to the program.");
//     }
// }