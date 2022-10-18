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
        int b = 3;
        Assert.False(a.Equals(b));
    }
};