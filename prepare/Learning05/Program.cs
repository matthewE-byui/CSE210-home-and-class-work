using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Test each shape individually first
        Square square = new Square("Red", 5);
        Console.WriteLine($"Square Color: {square.GetColor()}");
        Console.WriteLine($"Square Area: {square.GetArea()}");

        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Console.WriteLine($"Rectangle Color: {rectangle.GetColor()}");
        Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}");

        Circle circle = new Circle("Green", 3);
        Console.WriteLine($"Circle Color: {circle.GetColor()}");
        Console.WriteLine($"Circle Area: {circle.GetArea()}");

        Console.WriteLine("\n--- Using Polymorphism with List<Shape> ---\n");

        // List of Shapes
        List<Shape> shapes = new List<Shape>()
        {
            square,
            rectangle,
            circle
        };

        // Loop through them polymorphically
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Shape Color: {shape.GetColor()}");
            Console.WriteLine($"Shape Area: {shape.GetArea()}");
        }
    }
}
//testing git
