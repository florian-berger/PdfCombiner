using PdfCombiner.Resources.Language;
using Syncfusion.Pdf.Security;

namespace PdfCombiner.Model
{
    internal class EncryptionAlgorithmBinding
    {
        #region Properties

        /// <summary>
        ///     Algorithm for the encryption
        /// </summary>
        public PdfEncryptionAlgorithm Algorithm { get; }

        /// <summary>
        ///     Name that should be displayed in the UI
        /// </summary>
        public string DisplayName { get; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the binding object
        /// </summary>
        public EncryptionAlgorithmBinding(PdfEncryptionAlgorithm algorithm)
        {
            Algorithm = algorithm;
            DisplayName = GetTranslatedName(algorithm);
        }

        #endregion Constructor

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{DisplayName} ({Algorithm})";
        }

        #endregion Overrides

        #region Public methods

        /// <summary>
        ///     Returns the translated name for <see cref="PdfEncryptionAlgorithm" />
        /// </summary>
        public static string GetTranslatedName(PdfEncryptionAlgorithm algorithm)
        {
            var languageProperty = typeof(SettingsResource).GetProperty($"PdfEncryption_Algorithm_{algorithm}");
            if (languageProperty != null)
            {
                return (string) languageProperty.GetValue(null, null);
            }

            return algorithm.ToString();
        }

        #endregion Public methods
    }
}
