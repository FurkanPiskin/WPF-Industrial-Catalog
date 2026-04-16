using FerraFilterApp.Data;
using FerraFilterApp.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FerraFilterApp
{
    public partial class DetailPage : Page
    {
        private MainWindow _anaSayfa;
        private int _tıklananId;
        private FilterRepository _repository;

        // Ana sayfanın kumandasını tutuyoruz

        // Sayfa açılırken ana sayfayı içeri alıyoruz


        public DetailPage(MainWindow anaSayfa, int TıklananId)
        {
            InitializeComponent();
            _anaSayfa = anaSayfa;
            _tıklananId = TıklananId;
            _repository=new FilterRepository();
            Filtreler detay=_repository.GetFilterById(TıklananId);

            if (detay != null)
            {
                // Güvenli atama: veri varsa yazdır, yoksa boş string ("") döndür.
                foto1.Text = detay.foto1?.ToString() ?? "Resim Yok";
                ferra_no_bosluksuz.Text = detay.ferra_no_bosluksuz?.ToString() ?? "-";
                filtre_tipi_tr.Text = detay.filtre_tipi_tr?.ToString() ?? "-";

                // Ölçüler (a, b, c...)
                a.Text = VeriyiKontrolEt(detay.a);
                b.Text = VeriyiKontrolEt(detay.b);
                c.Text = VeriyiKontrolEt(detay.c);
                d.Text = VeriyiKontrolEt(detay.d);
                e.Text = VeriyiKontrolEt(detay.e);
                f.Text = VeriyiKontrolEt(detay.f);
                g.Text = VeriyiKontrolEt(detay.g);
                g1.Text = VeriyiKontrolEt(detay.g1);
                h.Text = VeriyiKontrolEt(detay.h);
                i.Text = VeriyiKontrolEt(detay.i);
                j.Text = VeriyiKontrolEt(detay.j);
                k.Text = VeriyiKontrolEt(detay.k);

                // Valfler ve Durum
                anti_drain.Text = VeriyiKontrolEt(detay.anti_drain);
                anti_syphon.Text = VeriyiKontrolEt(detay.anti_syphon);
                anti_syphon2.Text = VeriyiKontrolEt(detay.anti_syphon2);
                bypass.Text = VeriyiKontrolEt(detay.bypass);
                bypass2.Text = VeriyiKontrolEt(detay.bypass2);
                bypass3.Text = VeriyiKontrolEt(detay.bypass3);

                // XAML'da ismini 'filtredurumu' yapmıştık, modelde 'filtre_durumu'
                filtredurumu.Text = detay.filtre_durumu?.ToString() ?? "Bilinmiyor";

                // Başlık kısmını da güncelle
                txtSecilenFiltre.Text = detay.ferra_no_bosluksuz?.ToString() + " Detayları";
            }
            if (detay == null)
            {
                MessageBox.Show("DİKKAT: Veritabanından bu ID ile eşleşen hiçbir kayıt dönmedi! Sorguyu kontrol et.");
            }
            txtSecilenFiltre.Text =detay.ferra_no_bosluksuz;

            // FOTOĞRAF YÜKLEME MİMARİSİ (PRODUCTION STANDARDI)
            /*
            if (!string.IsNullOrEmpty(detay.foto1))
            {
                try
                {
                    // AMATÖR YOL: string yol = "C:\\Resimler\\" + detay.foto1;
                    // PROFESYONEL YOL: Yolu uygulamanın konfigürasyon dosyasından çekmek
                     string anaKlasor = Properties.Settings.Default.ResimKlasorYolu;
                    //string anaKlasor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotograflar");
                    // Klasör yolu ayarlardan boş gelirse veya klasör silinmişse defansif kontrol
                    if (string.IsNullOrEmpty(anaKlasor) || !System.IO.Directory.Exists(anaKlasor))
                    {
                        System.Diagnostics.Debug.WriteLine("HATA: Ayarlardaki resim klasörü bulunamadı!");
                        imgFiltre.Visibility = Visibility.Collapsed;
                        txtResimYok.Text = "Klasör Ayarı Eksik";
                        txtResimYok.Visibility = Visibility.Visible;
                        return; // Kodu burada kes, aşağı inip patlamasın
                    }

                    // Klasör yolu ile veritabanından gelen dosya adını güvenli şekilde birleştir
                    //string resimYolu = System.IO.Path.Combine(anaKlasor, detay.foto1);
                    string resimYolu = System.IO.Path.Combine(anaKlasor, detay.foto1);

                    if (System.IO.File.Exists(resimYolu))
                    {
                        // Noktalı virgül hatası düzeltildi
                        System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();

                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(resimYolu, UriKind.Absolute);
                        bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        // 1. Ana büyük resme veriyi bas
                        imgFiltre.Source = bitmap;

                        // 2. İŞTE YENİ KOD: Sol üstteki küçük önizleme kutusuna da aynı veriyi bas
                        imgThumb.Source = bitmap;

                        // Nesneleri görünür yap
                        imgFiltre.Visibility = Visibility.Visible;
                        txtResimYok.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        imgFiltre.Visibility = Visibility.Collapsed;
                        txtResimYok.Text = "Görsel Bulunamadı";
                        txtResimYok.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Resim yükleme hatası: " + ex.Message);
                    imgFiltre.Visibility = Visibility.Collapsed;
                    txtResimYok.Text = "Yükleme Hatası";
                    txtResimYok.Visibility = Visibility.Visible;
                }
            }
            */
            if (!string.IsNullOrEmpty(detay.foto1))
            {
                try
                {
                    // PROFESYONEL YOL: .exe dosyasının o an çalıştığı yeri bulur ve yanındaki "Fotograflar" klasörüne bakar.
                    string anaKlasor = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotograflar");

                    // Klasör fiziksel olarak yoksa defansif kontrol
                    if (!System.IO.Directory.Exists(anaKlasor))
                    {
                        System.Diagnostics.Debug.WriteLine("HATA: .exe'nin yanında 'Fotograflar' klasörü bulunamadı!");
                        imgFiltre.Visibility = Visibility.Collapsed;
                        txtResimYok.Text = "Klasör Bulunamadı";
                        txtResimYok.Visibility = Visibility.Visible;
                        // DİKKAT: Sayfanın geri kalanının çökmemesi için return KOMUTU BURADAN SİLİNDİ!
                    }
                    else
                    {
                        // Klasör yolu ile veritabanından gelen dosya adını güvenli şekilde birleştir
                        string resimYolu = System.IO.Path.Combine(anaKlasor, detay.foto1);

                        if (System.IO.File.Exists(resimYolu))
                        {
                            // Dosya kilitlenmesini engelleyen RAM dostu yükleme
                            System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage();

                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(resimYolu, UriKind.Absolute);
                            bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                            bitmap.EndInit();

                            // 1. Ana büyük resme veriyi bas
                            imgFiltre.Source = bitmap;

                            // 2. Sol üstteki küçük önizleme kutusuna da aynı veriyi bas
                            imgThumb.Source = bitmap;

                            // Nesneleri görünür yap
                            imgFiltre.Visibility = Visibility.Visible;
                            txtResimYok.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgFiltre.Visibility = Visibility.Collapsed;
                            txtResimYok.Text = "Görsel Bulunamadı";
                            txtResimYok.Visibility = Visibility.Visible;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Resim yükleme hatası: " + ex.Message);
                    imgFiltre.Visibility = Visibility.Collapsed;
                    txtResimYok.Text = "Yükleme Hatası";
                    txtResimYok.Visibility = Visibility.Visible;
                }
            }
            else
            {
                imgFiltre.Visibility = Visibility.Collapsed;
                txtResimYok.Text = "Görsel Yok";
                txtResimYok.Visibility = Visibility.Visible;
            }




            //Çarpraz referans Sayfasının Eklenmesi
            string elimdekiFerraNo = detay.ferra_no_bosluksuz;
            CrossReferencePage caprazSayfa = new CrossReferencePage(elimdekiFerraNo);
            frmCaprazReferans.Navigate(caprazSayfa);

        }

        private void btnGeri_Click(object sender, RoutedEventArgs e)
        {
            // Ana sayfadaki o yazdığımız metodu tetikle ve arama ekranını geri getir!
            _anaSayfa.AramaEkraninaDon();
        }
        // VERİ TEMİZLEYİCİ METOT: Gelen veri null, boş string ("") veya sadece boşluk (" ") ise tire (-) döndürür.
        private string VeriyiKontrolEt(object veri)
        {
            if (veri == null)
                return "-";

            // Baştaki ve sondaki gereksiz boşlukları kırp
            string metin = veri.ToString().Trim();

            if (string.IsNullOrWhiteSpace(metin))
                return "-";

            return metin;
        }
    }
}