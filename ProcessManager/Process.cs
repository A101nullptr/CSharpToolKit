using CSharpToolKit.LogManager;
using System;
using System.IO;
using System.Windows;

namespace CSharpToolKit.ProcessManager
{
    /// <summary>
    ///  Managing processes for starting and ending tasks.
    /// </summary>
    public class Process : IDisposable
    {
        private string _processName { get; set; }
        private Log _log { get; set; }

        /// <summary>
        /// Initializes a new instance of the Process class.
        /// </summary>
        /// <param name="task">Name of the process task.</param>
        /// <param name="logPath">Path for logging.</param>
        public Process(string task, string logPath)
        {
            _processName = task;
            _log = new Log(logPath); // Instantiates a logger with specified log path
        }

        /// <summary>
        /// Starts a process task.
        /// </summary>
        public void StartTask()
        {
            if (!IsExecutable())
                return;

            try
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(_processName);
                _log.WriteToLog($"ProcessManager::{process.ProcessName} has been activated successfully.");
            }
            catch (Exception ex)
            {
                _log.WriteToLog($"ProcessManager::Error:{ex.Message}");
            }
        }

        /// <summary>
        /// Ends a process task.
        /// </summary>
        public void EndTask()
        {
            if (!IsExecutable())
                return;

            string ProcessWithoutExtension = Path.GetFileNameWithoutExtension(_processName);
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(ProcessWithoutExtension);

            if (processes.Length == 0)
            {
                MessageBox.Show($"{_processName} is not active.", "Process Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (System.Diagnostics.Process process in processes)
            {
                try
                {
                    process.Kill();
                    _log.WriteToLog($"ProcessManager::{process.ProcessName} has been terminated successfully.");
                }
                catch (Exception ex)
                {
                    _log.WriteToLog($"ProcessManager::Error:{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Checks if the process is executable.
        /// </summary>
        /// <returns>True if the process is executable; otherwise, false.</returns>
        private bool IsExecutable()
        {
            _ = _log.CreateLog();

            if (_processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                return true;

            MessageBox.Show($"{_processName} does not exist.", "Process Error", MessageBoxButton.OK, MessageBoxImage.Information);

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
                _log.Dispose();
            }
        }
    }
}
