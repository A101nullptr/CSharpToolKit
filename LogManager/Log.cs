using System;
using System.IO;
using System.Windows;

namespace CSharpToolKit.LogManager
{

    /// <summary>
    /// Provides functionality to manage log files.
    /// </summary>
    public class Log
    {
        private string _filePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the Log class with the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the log file.</param>
        public Log(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            _filePath = filePath;
        }

        /// <summary>
        /// Creates a new log file.
        /// </summary>
        /// <returns>The path of the created log file.</returns>
        public string CreateLog()
        {
            if (File.Exists(_filePath))
                return _filePath;

            string CreatedPath = _filePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ? _filePath : $"{_filePath}.txt";
            File.Create(CreatedPath).Close();
            _filePath = CreatedPath;

            return _filePath;
        }

        /// <summary>
        /// Reads content from the log file.
        /// </summary>
        /// <returns>An array containing lines of the log file.</returns>
        public string[] ReadFromLog()
        {
            if (!IsExist() || !IsEmpty())
                return null;

            try
            {
                return File.ReadAllLines(_filePath);
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to read from log file.", ex);
            }
        }

        /// <summary>
        /// Writes a message to the log file.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteToLog(string message)
        {
            if (!IsExist())
                return;

            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to write to log file.", ex);
            }
        }

        /// <summary>
        /// Deletes the log file.
        /// </summary>
        public void DeleteLog()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to delete log file.", ex);
            }
        }

        /// <summary>
        /// Checks if the log file exists and has a valid .txt extension.
        /// </summary>
        /// <returns>True if the log file exists and has a valid extension; otherwise, false.</returns>
        private bool IsExist()
        {
            string extension = System.IO.Path.GetExtension(_filePath);

            if (extension is null || !extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"File {_filePath} does not have a valid file extension of .txt\n\nSelect a file with a valid extension.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!File.Exists(_filePath))
            {
                MessageBox.Show($"File {_filePath} does not exist.\n\nCreate the file then try again.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
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
            if (File.ReadAllLines(_filePath).Length == 0)
            {
                MessageBox.Show($"File {_filePath} is Empty.\n\nWrite to file then try again.", "Log Error", MessageBoxButton.OK, MessageBoxImage.Information);
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
