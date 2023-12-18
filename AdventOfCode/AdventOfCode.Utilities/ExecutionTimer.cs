using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Utilities
{
    /// <summary>
    /// Class for measuring the time taken for operations to complete.
    /// The total time of each type of operation is recorded along with each individual measurement.
    /// Can run multiple timers concurrently for benchmarking nested operations.
    /// </summary>
    public class ExecutionTimer
    {
        /// <summary>
        /// Dictionary of <see cref="Timer"/> instances.
        /// Each <see cref="Timer"/> instance represents benchmarking of a single operation.
        /// After a timer is started, the operation is run, then the timer is stopped again before recording the time in milliseconds.
        /// </summary>
        private Dictionary<string, Timer> Timers { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Timer"/> used for benchmarking the given operation.
        /// </summary>
        /// <param name="operation">The operation type name.</param>
        /// <returns>A <see cref="Timer"/> instance for the given operation.</returns>
        public Timer this[string operation]
        {
            get
            {
                return Timers[operation];
            }
        }

        /// <summary>
        /// Measures the time it takes for an operation to run.
        /// </summary>
        /// <param name="operation">The name of the operation and the key that will be used to create a <see cref="Timer"/> instance.</param>
        /// <param name="action">The action to be benchmarked.</param>
        public void Benchmark(string operation, Action action)
        {
            var timer = StartTimer(operation);
            action();
            timer.Stop();
        }

        /// <summary>
        /// Measures the time it takes for an operation to run and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The return type of the operation.</typeparam>
        /// <param name="operation">The name of the operation and the key that will be used to create a <see cref="Timer"/> instance.</param>
        /// <param name="function">The function to be benchmarked.</param>
        /// <returns>The result of the function that is run.</returns>
        public TResult Benchmark<TResult>(string operation, Func<TResult> function)
        {
            var timer = StartTimer(operation);
            TResult result = function();
            timer.Stop();

            return result;
        }

        /// <summary>
        /// Measures the time it takes for an asynchronous operation to run.
        /// </summary>
        /// <param name="operation">The name of the asynchronous operation and the key that will be used to create a <see cref="Timer"/> instance.</param>
        /// <param name="function">The function to be benchmarked.</param>
        /// <returns>A task representing the asynchronous function.</returns>
        public async Task BenchmarkAsync(string operation, Func<Task> function)
        {
            var timer = StartTimer(operation);
            await function();
            timer.Stop();
        }

        /// <summary>
        /// Measures the time it takes for an asynchronous operation to run and returns the result.
        /// </summary>
        /// <param name="operation">The name of the asynchronous operation and the key that will be used to create a <see cref="Timer"/> instance.</param>
        /// <param name="function">The function to be benchmarked.</param>
        /// <returns>A task representing the asynchronous function with the result.</returns>
        public async Task<TResult> BenchmarkAsync<TResult>(string operation, Func<Task<TResult>> function)
        {
            var timer = StartTimer(operation);
            TResult result = await function();
            timer.Stop();

            return result;
        }

        /// <summary>
        /// List the results of all <see cref="Timer"/> instances.
        /// </summary>
        /// <returns>A string containing the benchmark information of all timers.</returns>
        public string ListAllTimerResults()
        {
            var sb = new StringBuilder();
            foreach (var timer in Timers.Values)
            {
                sb.AppendLine(timer.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the timer for the given operation and starts it from zero.
        /// Creates a new timer instance if this is the first run.
        /// </summary>
        /// <param name="operation">The name of the type of operation.</param>
        /// <returns>A new or existing <see cref="Timer"/> instance that has been started.</returns>
        private Timer StartTimer(string operation)
        {
            // Create a new timer instance if one doesn't already exist
            if (!Timers.ContainsKey(operation)) Timers[operation] = new Timer(operation);

            var timer = Timers[operation];
            timer.Start(); // Start the timer
            return timer;
        }

        /// <summary>
        /// A <see cref="Timer"/> object used to measure the time for a single type of operation to complete.
        /// </summary>
        public class Timer
        {
            // Stopwatch instance used to time the operation.
            private Stopwatch Stopwatch { get; }

            /// <summary>
            /// The name of the operation type being benchmarked.
            /// </summary>
            public string OperationName { get; }

            /// <summary>
            /// List of individual measurements taken.
            /// Each measurement represents the time taken for a single run of the operation.
            /// </summary>
            public List<long> Benchmarks { get; }

            /// <summary>
            /// The total amount of time recorded for the operation to run.
            /// </summary>
            public long TotalTime => Benchmarks.Sum();

            /// <summary>
            /// True if the stopwatch is currently running.
            /// </summary>
            public bool IsRunning => Stopwatch.IsRunning;

            /// <summary>
            /// Creates a new <see cref="Timer"/> instance for the given operation type.
            /// </summary>
            /// <param name="operationName">The name used to represent the operation.</param>
            public Timer(string operationName)
            {
                Stopwatch = new();
                Benchmarks = new();
                OperationName = operationName;
            }

            /// <summary>
            /// Builds a message displaying the benchmarking results.
            /// </summary>
            /// <returns>A string containing the benchmark information.</returns>
            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Operation: {OperationName}");
                sb.AppendLine($"Total time: {TotalTime}ms");
                sb.AppendLine($"Number of runs: {Benchmarks.Count}");
                sb.AppendLine($"Average run time: {TotalTime / Benchmarks.Count}ms");
                sb.AppendLine($"Minimum run time: {Benchmarks.Min()}ms");
                sb.AppendLine($"Maximum run time: {Benchmarks.Max()}ms");
                return sb.ToString();
            }

            /// <summary>
            /// Restarts the stopwatch instance.
            /// </summary>
            internal void Start()
            {
                Stopwatch.Restart();
            }

            /// <summary>
            /// Stops the stopwatch instance and records a new benchmark time in the list.
            /// </summary>
            internal void Stop()
            {
                Stopwatch.Stop();
                Benchmarks.Add(Stopwatch.ElapsedMilliseconds);
            }
        }
    }
}