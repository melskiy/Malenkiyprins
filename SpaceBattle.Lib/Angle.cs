namespace SpaceBattle.Lib;

public class Angle
{
    private int numerator;
    private int denominator;
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
    public static int SCD(int a, int b)
    {
        if (a == 0) return b;
        if (b == 0) return a;
        if (a == b) return a;
        if (a == 1 || b == 1) return 1;
        if ((a % 2 == 0) && (b % 2 == 0)) return 2 * SCD(a / 2, b / 2);
        if ((a % 2 == 0) && (b % 2 != 0)) return SCD(a / 2, b);
        if ((a % 2 != 0) && (b % 2 == 0)) return SCD(a, b / 2);
        return SCD(b, Math.Abs(a - b));
    }
    public static Angle operator +(Angle a1, Angle a2)
    {
        int y3 = SCD(a1.numerator * a2.denominator + a2.numerator * a1.denominator, a1.denominator * a2.denominator);
        return new Angle((a1.numerator * a2.denominator + a2.numerator * a1.denominator) / y3, a1.denominator * a2.denominator / y3);
    }
    public static bool operator ==(Angle a1, Angle a2)
    {
        if (a1.numerator != a2.numerator || a1.denominator != a2.denominator)
        {
            return false;
        }
        return true;
    }
    public static bool operator !=(Angle a1, Angle a2)
    {
        return !(a1 == a2);
    }
    public override bool Equals(object? obj) => obj is Angle a && numerator == a.numerator && denominator == a.denominator;

    public override int GetHashCode()
    {
        return HashCode.Combine(numerator, denominator);
    }
}