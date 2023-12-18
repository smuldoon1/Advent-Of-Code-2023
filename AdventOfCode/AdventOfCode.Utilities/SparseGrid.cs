using System.Collections;

namespace AdventOfCode.Utilities
{
    public class SparseGrid<T> : IEnumerable<(VectorInt Position, T Value)>
    {
        private readonly Dictionary<VectorInt, T> dict;

        private int minX;
        private int maxX;
        private int minY;
        private int maxY;

        public VectorInt TopLeft => (minX, minY);
        public VectorInt TopRight => (maxX, minY);
        public VectorInt BottomLeft => (minX, maxY);
        public VectorInt BottomRight => (maxX, maxY);

        public int Count => dict.Count;

        public SparseGrid()
        {
            dict = new Dictionary<VectorInt, T>();
        }

        public T this[int x, int y]
        {
            get { return this[(x, y)]; }
            set { this[(x, y)] = value; }
        }

        public T this[VectorInt position]
        {
            get
            {
                return dict[position];
            }
            set
            {
                dict[position] = value;
                minX = Math.Min(minX, position.X);
                maxX = Math.Max(maxX, position.X);
                minY = Math.Min(minY, position.Y);
                maxY = Math.Max(maxY, position.Y);
            }
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(VectorInt position)
        {
            return dict.ContainsKey(position);
        }

        public T[,] ToArray()
        {
            if (minX < 0 || maxY < 0) throw new InvalidOperationException($"Cannot convert {nameof(SparseGrid<T>)} to an array as it contains negative indices.");

            var array = new T[maxX + 1, maxY + 1];
            foreach (var item in dict)
            {
                array[item.Key.X, item.Key.Y] = item.Value;
            }
            return array;
        }

        public IEnumerator<(VectorInt Position, T Value)> GetEnumerator()
        {
            foreach (var item in dict)
            {
                yield return (item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}