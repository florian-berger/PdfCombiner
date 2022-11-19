using System;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;

namespace PdfCombiner.Wpf.Extensions
{
    internal static class PdfDocumentExtensions
    {
        /// <summary>
        ///     Set the information for the encryption
        /// </summary>
        /// <param name="document">Document the encryption should be applied to</param>
        /// <param name="encryptionPassword">Password that should be used for encryption</param>
        public static void SetEncryptionInfo(this PdfLoadedDocument document, string encryptionPassword)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), @"The document must not be null!");
            }

            var security = document.Security;
            if (security != null)
            {
                security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContents;
                security.Algorithm = (PdfEncryptionAlgorithm) Enum.Parse(typeof(PdfEncryptionAlgorithm), App.Configuration.EncryptionAlgorithm);
                security.KeySize = (PdfEncryptionKeySize)Enum.Parse(typeof(PdfEncryptionKeySize), App.Configuration.EncryptionKeySize);

                security.UserPassword = encryptionPassword;
            }
        }

        /// <summary>
        ///     Set the compression settings
        /// </summary>
        /// <param name="document">Document the compression should be applied to</param>
        public static void SetCompressionInfo(this PdfLoadedDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), @"The document must not be null!");
            }

            var compressionOptions = new PdfCompressionOptions
            {
                CompressImages = App.Configuration.CompressImages,
                OptimizeFont = App.Configuration.OptimizeFonts,
                OptimizePageContents = App.Configuration.OptimizePageContents,
                RemoveMetadata = false
            };

            if (App.Configuration.CompressImages)
            {
                compressionOptions.ImageQuality = (int) App.Configuration.ImageQuality;
            }

            document.CompressionOptions = compressionOptions;
        }
    }
}
