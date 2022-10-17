namespace SpaceBattle.Lib;
public class Vector
{
    private int[] nums;
    public int Size
    {
        get => this.nums.Length;
    }
    public Vector(params int[] nums)
    {
        this.nums = nums;
    }
    public override string ToString()
    {
        return $"({string.Join(", ", this.nums)})";
    }
    public int this[int key]
    {
        get => this.nums[key];
        set => this.nums[key] = value;
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            throw new System.ArgumentException();
        }
        int[] vn = new int[v1.Size];
        for (int i = 0; i < v1.Size; i++)
        {
            vn[i] = v1[i] + v2[i];
        }
        return new Vector(vn);
    }
    public static bool operator ==(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            return false;
        }
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] != v2[i])
            {
                return false;
            }
        }
        return true;
    }
    public static bool operator !=(Vector v1, Vector v2)
    {
        return !(v1 == v2);
    }
    public override bool Equals(object? obj)
    {
        return obj is Vector v && nums.SequenceEqual(v.nums);
    }

    public override int GetHashCode()
    {
        return HashCode.nums;
    }
}