namespace SpaceBattle.Lib.Test;
public class Vectortest
{
    [Fact]
    public void VectorPositiveTest()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(25, 5);
        Assert.Equal(new Vector(30, 10), a + b);
    }
    [Fact]
    public void VectorNegativeSizeTest()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(25, 5, 4);
        Assert.Throws<ArgumentException>(() => (a + b));
    }

    [Fact]

    public void VectorPositivetoStringTest()
    {
        Vector a = new Vector(100, 5);
        Assert.Equal("(100, 5)", a.ToString());
    }
    [Fact]
    public void VectorNegativeEqualTest()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(25, 5);
        Assert.True(a != b);
    }
    [Fact]
    public void GetHashCodeVectorTest()
    {
        Vector a = new Vector(1, 2);
        Vector b = new Vector(1, 2);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
    [Fact]
    public void VectorNegativeEqvals()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(25, 5, 8);
        Assert.False(a == b);
    }
    [Fact]
    public void VectorPositiveeEqvals()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(5, 5);
        Assert.True(a.Equals(b));
    }
    [Fact]
    public void VectorGetTest()
    {
        Vector a = new Vector(5, 5);
        Assert.Equal(5, a[0]);
    }
    [Fact]
    public void VectorSetTest()
    {
        Vector a = new Vector(5, 5);
        a[0] = 3;
        Assert.Equal(3, a[0]);
    }
    [Fact]
    public void VectorPositiveEqvals()
    {
        Vector a = new Vector(5, 5);
        Vector b = new Vector(5, 5);
        Assert.True(a == b);
    }
    [Fact]
    public void VectornotEqvals()
    {
        Vector a = new Vector(5, 5);
        int b = 3;
        Assert.False(a.Equals(b));
    }
}