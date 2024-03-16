using System.Windows.Forms; 

namespace CSharpToolKit.BrowseManager
{
    /// <summary>
    /// Defines methods to browse for folders and files.
    /// </summary>
    interface IBrowseManager
    {
        /// <summary>
        /// Displays a dialog box that allows the user to browse for a folder.
        /// </summary>
        /// <returns>The path of the selected folder, or an empty string if no folder is selected.</returns>
        string LookUpFolder();

        /// <summary>
        /// Displays a dialog box that allows the user to browse for a file.
        /// </summary>
        /// <returns>The path of the selected file, or an empty string if no file is selected.</returns>
        string LookUpFile(); 
    }

    /// <summary>
    /// Implements the IBrowseManager interface for browsing folders and files.
    /// </summary>
    public class Browse : IBrowseManager
    {
        /// <inheritdoc/>
        public string LookUpFolder()
        {
            string FolderPath = string.Empty;  

            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    FolderPath = dialog.SelectedPath;
                }
            }

            return FolderPath;
        }

        /// <inheritdoc/>
        public string LookUpFile()
        {
            string filePath = string.Empty;

            using (var dialog = new OpenFileDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    filePath = dialog.FileName;
                }
            }

            return filePath;
        }
    }
}
