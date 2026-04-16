using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FerraFilterApp.Data;   // FilterRepository için gerekli
using FerraFilterApp.Models; // CrossReferenceModel için gerekli

namespace FerraFilterApp
{
    public partial class CrossReferencePage : Page
    {
        private string _aktifFerraNo;
        private FilterRepository _repository;
        private List<CrossReferenceModel> _tamListe;

        public CrossReferencePage(string ferraNoBosluksuz)
        {
            InitializeComponent();

            _aktifFerraNo = ferraNoBosluksuz;
            _repository = new FilterRepository();

            

            // Sayfa açıldığında otomatik olarak OEM (1) listesini doldur
            ListeyiDoldur(1);
        }

        private void ListeyiDoldur(int oemDurumu)
        {
            _tamListe = _repository.GetCrossReferences(_aktifFerraNo, oemDurumu);
            dgReferanslar.ItemsSource = _tamListe;

            // Eğer veritabanından liste boş dönmezse ComboBox'ı doldur
            if (_tamListe != null && _tamListe.Count > 0)
            {
                var benzersizMarkalar = _tamListe.Select(x => x.Uretici).Distinct().ToList();
                benzersizMarkalar.Insert(0, "Lütfen Seçiniz");
                cmbMarkaFiltre.ItemsSource = benzersizMarkalar;
                cmbMarkaFiltre.SelectedIndex = 0;
            }
            else
            {
                // Veri yoksa ComboBox'ı uyarı verecek şekilde ayarla
                cmbMarkaFiltre.ItemsSource = new List<string> { "Kayıt Bulunamadı" };
                cmbMarkaFiltre.SelectedIndex = 0;
            }
        }

        // İŞTE HATAYI ÇÖZECEK OLAN EKSİK METOT BURASI:
        private void cmbMarkaFiltre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Veri yoksa işlem yapma
            if (_tamListe == null || cmbMarkaFiltre.SelectedItem == null) return;

            string secilenMarka = cmbMarkaFiltre.SelectedItem.ToString();

            if (secilenMarka == "Lütfen Seçiniz" || secilenMarka == "Kayıt Bulunamadı")
            {
                dgReferanslar.ItemsSource = _tamListe;
            }
            else
            {
                // LINQ ile filtreleme yap
                var filtrelenmisListe = _tamListe.Where(x => x.Uretici == secilenMarka).ToList();
                dgReferanslar.ItemsSource = filtrelenmisListe;
            }
        }

        // OEM BUTONU TIKLAMASI
        private void btnOemNo_Click(object sender, RoutedEventArgs e)
        {
            btnOemNo.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#CC0000"));
            btnOemNo.Foreground = System.Windows.Media.Brushes.White;
            btnUretici.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EAEAEA"));
            btnUretici.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#555555"));

            ListeyiDoldur(1);
        }

        // ÜRETİCİ BUTONU TIKLAMASI
        private void btnUretici_Click(object sender, RoutedEventArgs e)
        {
            btnUretici.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#CC0000"));
            btnUretici.Foreground = System.Windows.Media.Brushes.White;
            btnOemNo.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EAEAEA"));
            btnOemNo.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#555555"));

            ListeyiDoldur(2);
        }
    }
}