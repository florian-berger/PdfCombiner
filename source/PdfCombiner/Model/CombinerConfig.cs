using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PdfCombiner.Helper;
using Syncfusion.Pdf.Security;

namespace PdfCombiner.Model
{
    /// <summary>
    ///     Configuration class for the PdfCombiner
    /// </summary>
    internal class CombinerConfig
    {
        #region Constants

        /// <summary>
        ///     List of all supported languages
        /// </summary>
        [JsonIgnore]
        public static readonly string[] SupportedLanguages = { "en-US", "de-DE" };

        /// <summary>
        ///     File name of the config file
        /// </summary>
        [JsonIgnore]
        private const string FileName = @"%appdata%/Florian Berger/PdfCombiner.json";

        #endregion Constants

        #region Properties

        /// <summary>
        ///     Language identifier that should be used
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Language { get; set; }

        /// <summary>
        ///     Information if PDF resources should be optimized while merging
        /// </summary>
        [JsonProperty(Order = 2)]
        public bool OptimizeResourcesWhileMerging { get; set; }

        /// <summary>
        ///     Information if PDF results should be encrypted after merging
        /// </summary>
        [JsonProperty(Order = 2)]
        public bool EncryptDocuments { get; set; }

        /// <summary>
        ///     Algorithm the results should be encrypted with
        /// </summary>
        [JsonProperty(Order = 3)]
        public string EncryptionAlgorithm { get; set; }

        /// <summary>
        ///     Key size the encryption should be executed with
        /// </summary>
        [JsonProperty(Order = 4)]
        public string EncryptionKeySize { get; set; }

        /// <summary>
        ///     Information if images should be compressed
        /// </summary>
        [JsonProperty(Order = 5)]
        public bool CompressImages { get; set; }

        /// <summary>
        ///     Information of the quality a compressed image should have
        /// </summary>
        [JsonProperty(Order = 6)]
        public double ImageQuality { get; set; }

        /// <summary>
        ///     Information if fonts should be optimized
        /// </summary>
        [JsonProperty(Order = 7)]
        public bool OptimizeFonts { get; set; }

        /// <summary>
        ///     Information if page contents should be optimized
        /// </summary>
        [JsonProperty(Order = 8)]
        public bool OptimizePageContents { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        ///     Loads the configuration. If the file is not existing, it is created
        /// </summary>
        public static CombinerConfig LoadConfig()
        {
            var fileName = Environment.ExpandEnvironmentVariables(FileName);

            if (!File.Exists(fileName))
            {
                var defaultConfig = new CombinerConfig
                {
                    Language = SupportedLanguages[0],
                    OptimizeResourcesWhileMerging = false,
                    EncryptDocuments = false,
                    EncryptionAlgorithm = PdfEncryptionAlgorithm.AES.ToString(),
                    EncryptionKeySize = PdfEncryptionKeySize.Key128Bit.ToString()
                };

                JsonFileHelper.SaveJsonFile(defaultConfig, fileName, true);

                return defaultConfig;
            }

            var config = JsonFileHelper.LoadJsonFile<CombinerConfig>(fileName);
            // If an unsupported language is saved, fallback to English
            if (SupportedLanguages.All(l => l != config.Language))
            {
                config.Language = SupportedLanguages[0];
            }

            if (!Enum.TryParse(config.EncryptionAlgorithm, out PdfEncryptionAlgorithm _))
            {
                config.EncryptionAlgorithm = PdfEncryptionAlgorithm.AES.ToString();
            }

            if (!Enum.TryParse(config.EncryptionKeySize, out PdfEncryptionKeySize _))
            {
                config.EncryptionKeySize = PdfEncryptionKeySize.Key128Bit.ToString();
            }

            return config;
        }

        /// <summary>
        ///     Saves the current configuration as json file
        /// </summary>
        public void Save()
        {
            var fileName = Environment.ExpandEnvironmentVariables(FileName);
            JsonFileHelper.SaveJsonFile(this, fileName, true);
        }

        #endregion Public methods
    }
}
