using FerraFilterApp.Data;
using FerraFilterApp.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerraFilterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Veritabanı ile konuşacak "Kuryemizi (Repository) sınıf seviyesinde tanımlıyoruz."
        private FilterRepository _repository;
        public MainWindow()
        {
            InitializeComponent();
            _repository = new FilterRepository();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // 1. Kullanıcının arama kutusuna yazdığı metni al
            string searchText = txtSearchBox.Text;



            // 2. Clean Code: Eğer kutu boşsa boşuna veritabanını yorma, kullanıcıyı uyar.
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Lütfen aramak için bir filtre veya OEM numarası girin.", "Bilgi Eksik", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Kodun aşağıya inmesini engeller, işlemi keser.
            }

            var allResults = _repository.SearchFilter(searchText);

            // 5. Kullanıcı Deneyimi (UX): Eğer sonuç bulunamadıysa bilgi ver
            if (allResults.Count == 0)
            {
                MessageBox.Show("Aradığınız kritere uygun filtre bulunamadı.", "Sonuç Yok", MessageBoxButton.OK, MessageBoxImage.Information);
                dgResults.ItemsSource = null;
                return;
            }

            List<SearchResultModel> finalResults = new List<SearchResultModel>();
            
            if (allResults.Any(x => x.SabitDegisken == "RD"))
            {
                finalResults = allResults.Where(x => x.SabitDegisken == "RD").ToList();
            }
            else if (allResults.Any(x => x.SabitDegisken == "ND")) {
                finalResults = allResults.Where(x => x.SabitDegisken == "ND").ToList();
            }

            else if (allResults.Any(x => x.SabitDegisken == "SD"))
            {
                // Kural 3: RD ve ND yok, SD varsa SADECE SD olanları al
                finalResults = allResults.Where(x => x.SabitDegisken == "SD").ToList();
            }
            else if (allResults.Any(x => x.SabitDegisken == "YD"))
            {
                // Kural 4: Hiçbiri yok, YD varsa SADECE YD olanları al
                finalResults = allResults.Where(x => x.SabitDegisken == "YD").ToList();
            }
            else
            {
                // Kural 5: Sadece DD (veya diğerleri) kaldıysa onları al
                finalResults = allResults.Where(x => x.SabitDegisken == "DD").ToList();
            }

            dgResults.ItemsSource = finalResults;
            
            /* Öncelik sıramızı kurumsal bir diziye (array) bağlıyoruz
            string[] oncelikSirasi = { "RD", "ND", "SD", "YD", "DD" };

            finalResults = new List<SearchResultModel>();

            // Sırayla kuralları kontrol et (En önemliden en önemsize doğru)
            foreach (var kural in oncelikSirasi)
            {
                // Tek bir seferde o kurala uyanları çek (Any ve Where birleşimi)
                var eslesenler = allResults.Where(x => x.SabitDegisken == kural).ToList();

                if (eslesenler.Count > 0)
                {
                    finalResults = eslesenler;
                    break; // Üst düzey bir kural eşleştiği an döngüyü kır, diğerlerine bakma!
                }
            }

            // Kör Nokta Koruması: Eğer yukarıdaki hiçbir kural eşleşmezse ve elimizde veri varsa, 
            // listeyi boşaltmak yerine ham halini (veya senin belirleyeceğin bir varsayılanı) ekrana bas.
            if (finalResults.Count == 0 && allResults.Count > 0)
            {
                finalResults = allResults;
            }

            dgResults.ItemsSource = finalResults;
            */






        }

        public void AramaEkraninaDon()
        {
            // Çerçeveyi kapat, eski arama ekranını tekrar aç
            frmAnaCerceve.Visibility = Visibility.Collapsed;
            pnlAramaEkrani.Visibility = Visibility.Visible;
        }

        private void FerraNo_Click(object sender, RoutedEventArgs e)
        {
            // 1. Tıklanan butonu yakala
            Button btn = sender as Button;

            // 2. Butonun içinde oturduğu tüm veri paketini (Row nesnesini) yakala
            // Bu satır sihirli; çünkü paketin içindeki tüm bilgilere (görünmeyen ID dahil) erişir.
            var secilenPaket = btn.DataContext as SearchResultModel;

            if (secilenPaket != null)
            {
                int tiklananId = secilenPaket.Id; // İşte o benzersiz ID artık elimizde!

                // 3. Geçiş yaparken artık numarayı değil, ID'yi gönderiyoruz
                pnlAramaEkrani.Visibility = Visibility.Collapsed;
                frmAnaCerceve.Visibility = Visibility.Visible;

                // Detay sayfasını ID ile besliyoruz
                frmAnaCerceve.Navigate(new DetailPage(this, tiklananId));
            }
        }
    }
}