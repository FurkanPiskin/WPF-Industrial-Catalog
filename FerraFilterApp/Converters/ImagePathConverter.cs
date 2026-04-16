using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FerraFilterApp.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dosyaAdi = value as string;
            if (string.IsNullOrEmpty(dosyaAdi)) return null;

            try
            {
                // .exe'nin çalıştığı klasörü bul ve "Fotograflar" ile birleştir
                string anaKlasor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotograflar");
                string resimYolu = Path.Combine(anaKlasor, dosyaAdi);

                if (File.Exists(resimYolu))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(resimYolu, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.DecodePixelWidth = 100; // Performans için küçük yükle
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { return null; }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}