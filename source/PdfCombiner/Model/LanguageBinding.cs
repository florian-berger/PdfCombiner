namespace PdfCombiner.Model
{
    /// <summary>
    ///     Binding object for the language configuration
    /// </summary>
    internal class LanguageBinding
    {
        #region Properties

        /// <summary>
        ///     Identifier of the language
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        ///     Name that should be displayed in the UI
        /// </summary>
        public string DisplayName { get; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the binding object
        /// </summary>
        public LanguageBinding(string identifier, string displayName)
        {
            Identifier = identifier;
            DisplayName = displayName;
        }

        #endregion Constructor

        #region Overrides

        public override string ToString()
        {
            return $"{DisplayName} ({Identifier})";
        }

        #endregion Overrides
    }
}
