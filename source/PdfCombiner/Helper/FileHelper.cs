using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PdfCombiner.Helper
{
    internal static class FileHelper
    {
        #region Constants

        /// <summary>
        ///     First 4 bytes of a PDF file (always starting with "%PDF")
        /// </summary>
        private static readonly byte[] ExpectedPdfBytes = { 0x25, 0x50, 0x44, 0x46 };

        #endregion Constants

        #region Internal methods

        /// <summary>
        ///     Checks if a file is of type PDF based on Magic numbers
        ///     https://en.wikipedia.org/wiki/Magic_number_(programming)#Magic_numbers_in_files
        /// </summary>
        /// <param name="fileName">File that should be checked</param>
        /// <returns>True if the file is a PDF</returns>
        /// <exception cref="FileNotFoundException">Thrown when the passed <paramref name="fileName"/> was not found</exception>
        internal static async Task<bool> IsFilePdfFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("The requested file was not found.", fileName);
            }

            var buffer = new byte[4];
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                _ = await fs.ReadAsync(buffer, 0, buffer.Length);
            }

            return ExpectedPdfBytes.SequenceEqual(buffer);
        }

        #endregion Internal methods
    }
}