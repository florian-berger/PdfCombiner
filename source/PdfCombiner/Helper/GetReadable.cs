namespace PdfCombiner.Helper
{
    /// <summary>
    ///     Returns a user readable text
    /// </summary>
    internal static class GetReadable
    {
        /// <summary>
        ///     Returns a user readable file size
        /// </summary>
        /// <param name="bytes">Number of bytes that should be made readable</param>
        public static string FileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            var order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes /= 1024;
            }

            return $"{bytes:0.##} {sizes[order]}";
        }
    }
}
