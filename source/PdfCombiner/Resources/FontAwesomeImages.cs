using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FontAwesome.Sharp;
using Color = System.Drawing.Color;

namespace PdfCombiner.Resources
{
    /// <summary>
    ///     Provides FontAwesome images as Bitmap
    /// </summary>
    internal static class FontAwesomeImages
    {
        /// <summary>
        ///     Green check mark
        /// </summary>
        internal static ImageSource ValidIcon => _validIcon ?? (_validIcon = BuildImageSource(IconChar.Check, Color.LightGreen));
        private static ImageSource _validIcon;
            //IconChar.Check.ToBitmap(IconFont.Regular, color: Color.Green);

        /// <summary>
        ///     Red cross
        /// </summary>
        internal static ImageSource InvalidIcon => _invalidIcon ?? (_invalidIcon = BuildImageSource(IconChar.Xmark, Color.Red));
        private static ImageSource _invalidIcon;

        private static ImageSource BuildImageSource(IconChar icon, Color color)
        {
            var bitmap = icon.ToBitmap(IconFont.Solid, color: color, size: 24);

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                var image = new BitmapImage();

                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                return image;
            }
        }
    }
}
