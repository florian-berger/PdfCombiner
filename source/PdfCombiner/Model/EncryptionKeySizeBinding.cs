using PdfCombiner.Resources.Language;
using Syncfusion.Pdf.Security;

namespace PdfCombiner.Model
{
    internal class EncryptionKeySizeBinding
    {
        #region Properties

        /// <summary>
        ///     Algorithm for the encryption
        /// </summary>
        public PdfEncryptionKeySize KeySize { get; }

        /// <summary>
        ///     Name that should be displayed in the UI
        /// </summary>
        public string DisplayName { get; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the binding object
        /// </summary>
        public EncryptionKeySizeBinding(PdfEncryptionKeySize keySize)
        {
            KeySize = keySize;
            DisplayName = GetTranslatedName(keySize);
        }

        #endregion Constructor

        #region Overrides

        public override string ToString()
        {
            return $"{DisplayName} ({KeySize})";
        }

        #endregion Overrides

        #region Public methods

        public static string GetTranslatedName(PdfEncryptionKeySize keySize)
        {
            var languageProperty = typeof(SettingsResource).GetProperty($"PdfEncryption_KeySize_{keySize}");
            if (languageProperty != null)
            {
                return (string) languageProperty.GetValue(null, null);
            }

            return keySize.ToString();
        }

        #endregion Public methods
    }
}
