using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FerraFilterApp.Models;

namespace FerraFilterApp.Data
{
    public class FilterRepository
    {
        // App.config dosyasından SQL adresimizi alıyoruz
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FerraConnection"].ConnectionString;

        // Ana arama fonksiyonumuz.
        public List<SearchResultModel> SearchFilter(string searchText)
        {
            // Kullanıcının girdiği metindeki boşlukları ve özel karakterleri temizliyoruz
            string cleanSearch = searchText.Replace(".", "")
                                           .Replace("-", "")
                                           .TrimStart('0')
                                           .Trim();

            // 'using' bloğu, işlem bitince veritabanı bağlantısını otomatik kapatır
            using (var connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
            SELECT 
                m.id AS Id,
                m.filtre_no_b AS OrijinalNo, 
                m.firma_adi AS Uretici, 
                f.ferra_no_bosluksuz AS FerraNo, 
                f.filtre_durumu AS Durum, 
                f.foto1 AS Fotograf,
                m.sabit_degisken AS SabitDegisken   
            FROM Ferra_MuadilOrijinal m
            INNER JOIN Filtreler f ON m.ferra_no_b = f.ferra_no_bosluksuz
            WHERE m.filtre_no_goster LIKE @SearchKey"; 
               

                // MÜHENDİSLİK DOKUNUŞU BURADA:
                // Baştaki '%' işaretini kaldırdık. Artık sadece girilen harf/kelime ile BAŞLAYANLARI getirecek.
                var results = connection.Query<SearchResultModel>(
                    sqlQuery,
                    new { SearchKey = $"{cleanSearch}%" }
                ).ToList();

                return results;
            }
        }
        public Filtreler GetFilterById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT f.* FROM Filtreler f
            INNER JOIN Ferra_MuadilOrijinal m ON f.ferra_no_bosluksuz = m.ferra_no_b
            WHERE m.id = @Id";
                // Dapper, gelen bütün kolonları senin "Filtreler.cs" modelindeki özelliklerle (Property) isimlerine bakarak otomatik eşleştirir.
                return connection.QuerySingleOrDefault<Filtreler>(sql, new { Id = id });

            }
        }
        public List<CrossReferenceModel> GetCrossReferences(string ferraNo,int oemDurumu)
        {
            using (var connection = new SqlConnection(_connectionString)) {

                string sql = @"
            SELECT firma_adi AS Uretici, filtre_no_goster AS RakipNo 
            FROM Ferra_MuadilOrijinal
            WHERE ferra_no_b = @FerraNo AND orjinal_muadil = @OemDurumu";

                return connection.Query<CrossReferenceModel>(sql, new { FerraNo = ferraNo, OemDurumu = oemDurumu }).ToList();
            }
        }
    }
}