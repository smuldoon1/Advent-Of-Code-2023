using System.Collections;

namespace AdventOfCode.Utilities
{
    public class Grid<T> : IEnumerable<T>
    {
        private readonly T[,] _grid;

        public readonly int Width;

        public readonly int Height;

        public T this[int x, int y]
        {
            get
            {
                if (!IsInRange(x, y)) throw new IndexOutOfRangeException($"Grid index is out of range. Consider using the {nameof(TryGet)} method if you do not want to throw an error when out of range.");
                return _grid[x, y];
            }
            set
            {
                if (!IsInRange(x, y)) throw new IndexOutOfRangeException("Grid index is out of range.");
                _grid[x, y] = value;
            }
        }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _grid = new T[Width, Height];
        }

        /// <summary>
        /// Tries to get the value of the element at [<paramref name="x"/>, <paramref name="y"/>].
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The instance of <typeparamref name="T"/> at the given coordinate, or the default value of <typeparamref name="T"/> if out of range.</returns>
        public T? TryGet(int x, int y)
        {
            if (!IsInRange(x, y)) return default;
            return _grid[x, y];
        }

        /// <summary>
        /// Tries to set the value of the element at [<paramref name="x"/>, <paramref name="y"/>].
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="value">The value to set the element to.</param>
        /// <returns>True if the element was set. False if the coordinate is out of range.</returns>
        public bool TrySet(int x, int y, T value)
        {
            if (!IsInRange(x, y)) return false;
            _grid[x, y] = value;
            return true;
        }

        /// <summary>
        /// Initialises all elements in the grid using the default constructor of type <typeparamref name="T"/>.
        /// </summary>
        public void CreateInstances()
        {
            if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        _grid[x, y] = Activator.CreateInstance<T>();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a given x and y coordinate is in range of the grid.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>True if the coordinate is in range.</returns>
        public bool IsInRange(int x, int y)
        {
            return x > 0 && y > 0 && x < Width && y < Height;
        }

        public T[] GetColumn(int x) => GetColumn(x, 0, Height - 1);

        public T[] GetColumn(int x, int start) => GetColumn(x, start, Height);

        public T[] GetColumn(int x, int start, int end)
        {
            var result = new List<T>();
            for (int y = start; y < end; y++)
            {
                result.Add(_grid[x, y]);
            }
            return result.ToArray();
        }

        public T[] GetRow(int y) => GetRow(y, 0, Height - 1);

        public T[] GetRow(int y, int start) => GetRow(y, start, Height);

        public T[] GetRow(int y, int start, int end)
        {
            var result = new List<T>();
            for (int x = start; x < end; x++)
            {
                result.Add(_grid[x, y]);
            }
            return result.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in _grid)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
