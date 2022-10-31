using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PdfCombiner.Helper;

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
                    OptimizeResourcesWhileMerging = false
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
