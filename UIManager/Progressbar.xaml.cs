using System;
using System.Windows;
using System.Windows.Input;

namespace CSharpToolKit.UIManager
{
    /// <summary>
    /// Represents a custom progress bar window for displaying progress of a task.
    /// </summary>
    public partial class Progressbar : Window, IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the progress bar window is closed.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Progressbar"/> class.
        /// </summary>
        /// <param name="title">Optional. The title of the progress bar window.</param>
        /// <param name="maximum">Optional. The maximum value of the progress bar.</param>
        public Progressbar(string title = "", double maximum = 100.0)
        {
            InitializeComponent();
            InitializeSize();
            UpdateTitle(title);
            processLabel.Content = title;
            this.progressBar.Maximum = maximum;
            this.Closed += (s, e) =>
            {
                IsClosed = true;
            };
        }

        /// <summary>
        /// Initializes the size and position of the progress bar window.
        /// </summary>
        private void InitializeSize()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Updates the title of the progress bar window.
        /// </summary>
        /// <param name="title">The new title of the progress bar window.</param>
        public void UpdateTitle(string title)
        {
            this.Title = title;
            processLabel.Content = title;
        }

        /// <summary>
        /// Handles the left mouse button down event to enable window dragging.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The mouse button event arguments.</param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            DragMove();

        }

        /// <summary>
        /// Updates the progress bar value.
        /// </summary>
        /// <param name="value">Optional. The value to increment the progress bar by.</param>
        /// <returns>True if the progress bar window is closed, otherwise false.</returns>
        public bool Update(double value = 1.0)
        {
            DoEvents();
            progressBar.Value += value;
            return IsClosed;
        }

        /// <summary>
        /// Executes any pending events in the application's message queue.
        /// </summary>
        private void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        /// <summary>
        /// Disposes of the progress bar window.
        /// </summary>
        public void Dispose()
        {
            if (!IsClosed) Close();
        }
    }
}