using System;
public class Fraction
{
    // private attributes
    private int _top;
    private int _bottom;

    // constructios

    //no-arfument constructor
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }
    //constructor for a whole number
    public Fraction(int wholeNumber)
    {
        _top = wholeNumber;
        _bottom = 1;
    }
    //constructor for a top and bottom
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }
    //getters and setters 
    public int GetTop()
    {
        return _top;

    }

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom()
    {
        return _bottom;

    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // other methods 
    // return 3/4
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    //return decimal value 3/4 -> 0.75
    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }



    
}