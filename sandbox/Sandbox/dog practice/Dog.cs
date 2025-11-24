

public class Dog
{
    private string _name;
    private int _age;

    public Dog(string name, int age)
    {
        _name = name;
        _age = age;

    }
    public void Bark()
    {
        Console.WriteLine($"{_name} says: Woof!");

    }
    public void HaveBirthday()
    {
        _age++;
        Console.WriteLine($"{_name} is now {_age} years old!");
    }
}