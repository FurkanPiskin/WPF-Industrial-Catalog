using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FerraFilterApp
{
    // IValueConverter arayüzü, bu sınıfın bir XAML çevirmeni olduğunu belirtir
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Veritabanından gelen dosya adı boş mu kontrolü
            string dosyaAdi = value as string;
            if (string.IsNullOrEmpty(dosyaAdi))
                return null; // Resim yoksa boş dönsün

            try
            {
                // Ayarlardaki klasör yolunu al
                string anaKlasor = Properties.Settings.Default.ResimKlasorYolu;
                if (string.IsNullOrEmpty(anaKlasor)) return null;

                string resimYolu = Path.Combine(anaKlasor, dosyaAdi);

                // Gerçekten böyle bir dosya var mı?
                if (File.Exists(resimYolu))
                {
                    // Tıpkı detay sayfasında yaptığın gibi resmi kilitlemeden RAM'e al
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(resimYolu, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // Dosya kilitlenmesini önler
                    bitmap.DecodePixelWidth = 100; // ÖNEMLİ: Tabloda kasmaması için resmi küçük çözünürlükte yükle!
                    bitmap.EndInit();

                    return bitmap;
                }
            }
            catch
            {
                return null; // Hata durumunda program çökmesin, resmi boş göstersin
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}