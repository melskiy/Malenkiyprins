namespace SpaceBattle.Lib.Test;
public class AngleTest
{
    [Fact]
    public void AngleTestSum()
    {
        Angle a = new Angle(1, 5);
        Angle b = new Angle(4, 3);
        Angle c = new Angle(3, 9);
        Angle d = new Angle(3, 5);
        Assert.Equal(new Angle(23, 15), a + b);
        Assert.Equal(new Angle(5, 3), c + b);
        Assert.Equal(new Angle(5, 3), b + c);
        Assert.Equal(new Angle(29, 15), b + d);
    }
    
    [Fact]
    public void AngleTestEqual()
    {
        Assert.Throws<ArgumentException>(() => new Angle(1, 0));
    }

    [Fact]
    public void AngleEqualEqualTest()
    {
        Angle a = new Angle(1, 5);
        Angle b = new Angle(4, 3);
        Assert.True(a != b);
    }

    [Fact]
    public void AngleNotEqualTest()
    {
        Angle a = new Angle(4, 3);
        Angle b = new Angle(4, 3);
        Assert.True(a == b);

    }

    [Fact]
    public void VectorPositiveNotEqualsTest()
    {
        Angle a = new Angle(45, 1);
        Assert.Equal("45 / 1", a.ToString());
    }

    [Fact]
    public void GetHashCodeTest()
    {
        Angle a = new Angle(1, 2);
        Angle b = new Angle(1, 2);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void EqvalsNegativeTest()
    {
        Angle a = new Angle(1, 2);
        Angle c = new Angle(2, 1);
        int b = 3;
        Assert.False(a.Equals(b));
        Assert.False(a.Equals(c));
    }

    [Fact]
    public void DecTets()
    {
        Assert.Equal(2, Angle.GCD(2, 2));
    }
<<<<<<< HEAD:SpaceBattle.Lib.Test/Angletest.cs
};
=======

};
>>>>>>> 4e6b53ca86f86abac59df7e9408c1bb6f051c7d1:SpaceBattle.Lib.Test/AngleTest.cs
