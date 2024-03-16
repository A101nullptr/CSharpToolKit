using System;
using System.IO;
using System.Windows;

namespace CSharpToolKit.LogManager
{
    /// <summary>
    /// Provides functionality to manage log files.
    /// </summary>
    public interface ILogManager : IDisposable
    {
        /// <summary>
        /// Creates a new log file.
        /// </summary>
        /// <returns>The path of the created log file.</returns>
        string CreateLog();

        /// <summary>
        /// Reads content from the log file.
        /// </summary>
        /// <returns>An array containing lines of the log file.</returns>
        string[] ReadFromLog();

        /// <summary>
        /// Writes a message to the log file.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        void WriteToLog(string message);

        /// <summary>
        /// Deletes the log file.
        /// </summary>
        void DeleteLog();
    }

    /// <summary>
    /// Implements the ILogManager interface for managing log files.
    /// </summary>
    public class Log : ILogManager
    {
        private string Path { get; set; }

        /// <summary>
        /// Initializes a new instance of the Log class with the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the log file.</param>
        public Log(string filePath)
        {
            Path = filePath;
        }

        /// <inheritdoc/>
        public string CreateLog()
        {
            if (File.Exists(Path))
            {
                MessageBox.Show($"File {Path} already exists.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return Path;
            }

            string CreatedPath = Path.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ? Path : $"{Path}.txt";
            File.Create(CreatedPath).Close();
            Path = CreatedPath;

            return Path;
        }

        /// <inheritdoc/>
        public string[] ReadFromLog()
        {
            if (!IsExist() || !IsEmpty())
                return null;

            return File.ReadAllLines(Path);
        }

        /// <inheritdoc/>
        public void WriteToLog(string message)
        {
            if (!IsExist())
                return;

            using (StreamWriter writer = File.AppendText(Path))
            {
                writer.WriteLine(message);
            }
        }

        /// <inheritdoc/>
        public void DeleteLog()
        {
            if (IsExist())
            {
                File.Delete(Path);
                Path = null;
            }
        }

        /// <summary>
        /// Checks if the log file exists and has a valid .txt extension.
        /// </summary>
        /// <returns>True if the log file exists and has a valid extension; otherwise, false.</returns>
        private bool IsExist()
        {
            string extension = System.IO.Path.GetExtension(Path);

            if (extension is null || !extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"File {Path} does not have a valid file extension of .txt\n\nSelect a file with a valid extension.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!File.Exists(Path))
            {
                MessageBox.Show($"File {Path} does not exist.\n\nCreate the file then try again.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the log file is empty.
        /// </summary>
        /// <returns>True if the log file is not empty; otherwise, false.</returns>
        private bool IsEmpty()
        {
            if (File.ReadAllLines(Path).Length == 0)
            {
                MessageBox.Show($"File {Path} is Empty.\n\nWrite to file then try again.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Disposes of resources used by the Log instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of managed and unmanaged resources used by the Log instance.
        /// </summary>
        /// <param name="disposing">True if called from Dispose(), false if called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
            }
            // Dispose unmanaged resources
        }

    }
}
