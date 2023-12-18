namespace AdventOfCode.Utilities
{
    public struct VectorInt
    {
        public int X { get; set; }
        public int Y { get; set; }

        public VectorInt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator == (VectorInt a, VectorInt b) =>
            a.X == b.X && a.Y == b.Y;

        public static bool operator != (VectorInt a, VectorInt b) =>
            a.X != b.X || a.Y != b.Y;

        public static bool operator == (VectorInt a, ValueTuple<int, int> b) =>
            a.X == b.Item1 && a.Y == b.Item2;

        public static bool operator !=(VectorInt a, ValueTuple<int, int> b) =>
            a.X != b.Item1 || a.Y != b.Item2;

        public static VectorInt operator + (VectorInt a, VectorInt b) =>
            new(a.X + b.X, a.Y + b.Y);

        public static VectorInt operator - (VectorInt a, VectorInt b) =>
            new(a.X - b.X, a.Y - b.Y);

        public static VectorInt operator * (VectorInt a, VectorInt b) =>
            new(a.X * b.X, a.Y * b.Y);

        public static VectorInt operator * (VectorInt a, int b) =>
            new(a.X * b, a.Y * b);

        public static VectorInt operator / (VectorInt a, VectorInt b) =>
            new(a.X / b.X, a.Y / b.Y);

        public static VectorInt operator / (VectorInt a, int b) =>
            new(a.X / b, a.Y / b);

        public static VectorInt operator - (VectorInt a) =>
            new(-a.X, -a.Y);

        public static implicit operator VectorInt((int x, int y) tuple) =>
            new(tuple.x, tuple.y);

        public static implicit operator (int x, int y)(VectorInt vector) =>
            (vector.X, vector.Y);

        public override bool Equals(object? obj)
        {
            if (obj is VectorInt other) return this == other;
            else if (obj is ValueTuple<int, int> tuple) return this == tuple;

            return false;
        }

        public override int GetHashCode() =>
            17 * (23 + X.GetHashCode()) * (29 + Y.GetHashCode());

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public double Magnitude() =>
            Math.Sqrt(X * X + Y * Y);

        public static int Distance(VectorInt a, VectorInt b) =>
            Math.Abs(a.X - b.X + a.Y - b.Y);
    }
}