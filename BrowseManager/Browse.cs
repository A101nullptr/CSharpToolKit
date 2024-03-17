using System.Windows.Forms; 

namespace CSharpToolKit.BrowseManager
{
    /// <summary>
    /// For browsing folders and files.
    /// </summary>
    public class Browse
    {
        /// <summary>
        /// Displays a dialog box that allows the user to browse for a folder.
        /// </summary>
        /// <returns>The path of the selected folder, or an empty string if no folder is selected.</returns>
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

        /// <summary>
        /// Displays a dialog box that allows the user to browse for a file.
        /// </summary>
        /// <returns>The path of the selected file, or an empty string if no file is selected.</returns>
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
