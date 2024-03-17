using System;
using System.Diagnostics;

namespace CSharpToolKit.TimeManager
{
    /// <summary>
    /// Represents a timer implementation using Stopwatch.
    /// </summary>
    public class Timer
    {
        private Stopwatch Stopwatch { get; }

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        public Timer()
        {
            Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void TimerStart()
        {
            Stopwatch.Start();
        }

        /// <summary>
        /// Stops the timer and returns the elapsed time.
        /// </summary>
        /// <param name="id">Identifier for specifying the format of the elapsed time string.
        /// /// <list type="table">
        /// <item><term>0</term><description>HH:mm:ss</description></item>
        /// <item><term>1</term><description>HHmmss</description></item>
        /// <item><term>-1</term><description>HH_mm_ss</description></item>
        /// <item><term>Default</term><description>HHmmss</description></item>
        /// </list>
        /// </param>
        /// <returns>The elapsed time formatted as a string.</returns>
        public string TimerStop(int id)
        {
            if (!Stopwatch.IsRunning)
                Stopwatch.Start();

            Stopwatch.Stop();
            string time;
            TimeSpan elapsedTime = Stopwatch.Elapsed;
            switch (id)
            {
                case 0:
                    time = elapsedTime.ToString(@"hh\:mm\:ss");
                    break;
                case 1:
                default:
                    time = elapsedTime.ToString(@"hhmmss");
                    break;
                case -1:
                    time = elapsedTime.ToString(@"hh\_mm\_ss");
                    break;
            }

            return time;
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void TimerReset()
        {
            if (Stopwatch.IsRunning)
                Stopwatch.Stop();

            Stopwatch.Reset();
        }
    }
}
