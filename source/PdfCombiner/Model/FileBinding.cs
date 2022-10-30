using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using DevExpress.Mvvm;
using PdfCombiner.Helper;
using PdfCombiner.Resources;

namespace PdfCombiner.Model
{
    /// <summary>
    ///     Binding class for files that were added for combination
    /// </summary>
    internal class FileBinding : BindableBase
    {
        #region Properties

        /// <summary>
        ///     Name of the file
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value, () => FileName);
        } private string _fileName;

        /// <summary>
        ///     Full location of the file
        /// </summary>
        public string FileLocation
        {
            get => _fileLocation;
            set => SetProperty(ref _fileLocation, value, () => FileLocation);
        } private string _fileLocation;

        /// <summary>
        ///     Size of the file
        /// </summary>
        public long FileSize
        {
            get => _fileSize;
            set => SetProperty(ref _fileSize, value, () => FileSize);
        } private long _fileSize;

        /// <summary>
        ///     Information if the file is a valid PDF file
        /// </summary>
        public bool IsValidPdfFile
        {
            get => _isValidPdfFile;
            set => SetProperty(ref _isValidPdfFile, value, () => IsValidPdfFile);
        } private bool _isValidPdfFile;

        /// <summary>
        ///     Icon for the validity state
        /// </summary>
        // ReSharper disable once UnusedMember.Global - Used in XAML
        public ImageSource ValidIcon => IsValidPdfFile ? FontAwesomeImages.ValidIcon : FontAwesomeImages.InvalidIcon;

        #endregion Properties

        #region Public methods

        public static async Task<FileBinding> Create(string fileName)
        {
            var isValidPdfFile = await FileHelper.IsFilePdfFile(fileName);
            var fileInfo = new FileInfo(fileName);

            return new FileBinding
            {
                FileLocation = fileName,
                FileName = Path.GetFileName(fileName),
                IsValidPdfFile = isValidPdfFile,
                FileSize = fileInfo.Length
            };
        }

        #endregion Public methods

        #region Overrides

        public override string ToString()
        {
            return FileName;
        }

        #endregion Overrides
    }
}
