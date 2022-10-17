namespace SpaceBattle.Lib;
public class Angle
{
    private int numerator;
    private int denominator;
    public Angle(int n, int d)
    {
        this.numerator = n;
        this.denominator = d;
    }
    public override string ToString()
    {
        return $"{this.numerator} / {this.denominator}";
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        static int Znamen(int y1, int y2)
        {
            if ((y2 >= y1) && (y2 % y1 == 0))
            {
                return y2;
            }
            else if ((y1 > y2) && (y1 % y2 == 0))
            {
                return y1;
            }
            return y2 * y1;
        }
        int x3, y3;
        y3 = Znamen(a1.denominator, a2.denominator);
        if (y3 == a1.denominator || y3 == a2.denominator)
        {
            return new Angle(a1.numerator * (a2.denominator / y3) + a2.numerator * (a1.denominator / y3), y3);
        }
        return new Angle(x3 = a1.numerator * a2.denominator + a2.numerator * a1.denominator, y3);
    }
}