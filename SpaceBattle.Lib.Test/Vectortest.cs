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
        Vector b = new Vector(25,5);
        Assert.True(a != b);
    }
    [Fact]
    public void GetHashCodeVectorTest()
    {
        Vector a = new Vector(1, 2);
        Vector b = new Vector(1, 2);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}