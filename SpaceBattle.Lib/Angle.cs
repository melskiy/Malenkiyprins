namespace SpaceBattle.Lib;
public class Angle
{
    public int numerator;
    public int denominator;
    public Angle(int n, int d)
    {
        this.numerator = n;
        this.denominator = d;
        if (d == 0)
        {
            throw new System.ArgumentException();
        }
    }
    public override string ToString()
    {
        return $"{this.numerator} / {this.denominator}";
    }
    private static int GCD(int a, int b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
    public static Angle operator +(Angle a1, Angle a2)
    {
        int y3 = GCD(a1.numerator * a2.denominator + a2.numerator * a1.denominator, a1.denominator * a2.denominator);
        return new Angle((a1.numerator * a2.denominator + a2.numerator * a1.denominator) / y3, a1.denominator * a2.denominator / y3);
    }
    public static bool operator ==(Angle a1, Angle a2)
    {
        return !(a1.numerator != a2.numerator || a1.denominator != a2.denominator);
    }
    public static bool operator !=(Angle a1, Angle a2)
    {
        return !(a1 == a2);
    }
    public override bool Equals(object? obj)
    {
        return obj is Angle a &&
         numerator == a.numerator &&
         denominator == a.denominator;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(numerator, denominator);
    }
}