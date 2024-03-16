using CSharpToolKit.LogManager;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;

namespace CSharpToolKit.ProcessManager
{
    /// <summary>
    /// Interface for managing processes, inherits IDisposable.
    /// </summary>
    interface IProcessManager : IDisposable
    {
        /// <summary>
        /// Starts a process task.
        /// </summary>
        void StartTask();

        /// <summary>
        /// Ends a process task.
        /// </summary>
        void EndTask();
    }

    /// <summary>
    /// Represents a process manager for starting and ending tasks.
    /// </summary>
    public class Process : IProcessManager
    {
        private string ProcessName { get; set; }
        private ILogManager LogManager { get; set; }

        /// <summary>
        /// Initializes a new instance of the Process class.
        /// </summary>
        /// <param name="task">Name of the process task.</param>
        /// <param name="logPath">Path for logging.</param>
        public Process(string task, string logPath)
        {
            ProcessName = task;
            LogManager = new Log(logPath); // Instantiates a logger with specified log path
        }

        /// <inheritdoc/>
        public void StartTask()
        {
            if (!IsExecutable())
                return;

            try
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(ProcessName);
                LogManager.WriteToLog($"{process.ProcessName} has been activated successfully.");
            }
            catch (Exception ex)
            {
                LogManager.WriteToLog($"Error: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public void EndTask()
        {
            if (!IsExecutable())
                return;

            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(ProcessName);

            if (processes.Length == 0)
            {
                MessageBox.Show($"{ProcessName} is not active.", "Process Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (System.Diagnostics.Process process in processes)
            {
                try
                {
                    process.Kill();
                    LogManager.WriteToLog($"{process.ProcessName} has been terminated successfully.");
                }
                catch (Exception ex)
                {
                    LogManager.WriteToLog($"Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Checks if the process is executable.
        /// </summary>
        /// <returns>True if the process is executable; otherwise, false.</returns>
        private bool IsExecutable()
        {
            _ = LogManager.CreateLog();

            if (ProcessName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                return true;

            MessageBox.Show($"{ProcessName} does not exist.", "Process Error", MessageBoxButton.OK, MessageBoxImage.Information);

            return false;
        }

        /// <summary>
        /// Disposes of resources used by the Process instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of managed and unmanaged resources used by the Process instance.
        /// </summary>
        /// <param name="disposing">True if called from Dispose(), false if called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LogManager.Dispose();
            }
        }
    }
}
