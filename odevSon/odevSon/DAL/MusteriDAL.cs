using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using static odev2.EntityLayer.MusteriEntityLayer;

namespace odev2.DAL
{
    public class MusteriDAL
    {
        public void MusteriEkle(string musteriAdiSoyadi, int musteriTelNo, int musteriTcNo, string musteriOdemeTuru)
        {
            DbBaglanti dbBaglanti = new DbBaglanti();

            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "INSERT INTO musteri (musteri_adi_soyadi, musteri_telNo, musteri_tcNo, musteri_odemeTuru) VALUES (@AdiSoyadi, @TelNo, @TcNo, @OdemeTuru)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdiSoyadi", musteriAdiSoyadi);
                command.Parameters.AddWithValue("@TelNo", musteriTelNo);
                command.Parameters.AddWithValue("@TcNo", musteriTcNo);
                command.Parameters.AddWithValue("@OdemeTuru", musteriOdemeTuru);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Müşteri eklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        public void MusteriGuncelle(string yeniAdSoyad, int tcNo, int yeniTelNo, string yeniOdemeTuru)
        {
            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "UPDATE musteri SET AdSoyad = @AdSoyad, TelefonNo = @TelefonNo, OdemeTuru = @OdemeTuru WHERE TcNo = @TcNo";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdSoyad", yeniAdSoyad);
                command.Parameters.AddWithValue("@TelefonNo", yeniTelNo);
                command.Parameters.AddWithValue("@OdemeTuru", yeniOdemeTuru);
                command.Parameters.AddWithValue("@TcNo", tcNo);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void MusteriSil(string tcNo)
        {
            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "DELETE FROM musteri WHERE TcNo = @TcNo";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TcNo", tcNo);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Musteri> MusteriListele()
        {
            List<Musteri> musteriler = new List<Musteri>();

            using (MySqlConnection connection = DbBaglanti.BaglantiGetir()) // Bağlantıyı aç
            {
                string query = "SELECT Id, AdSoyad, TelNo, TcNo, OdemeTuru FROM musteri"; // Veritabanındaki müşteri verilerini al
                MySqlCommand command = new MySqlCommand(query, connection); // MySqlCommand kullanıldı

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader(); // Veritabanındaki verileri oku

                while (reader.Read()) // Her satırı okur
                {
                    Musteri musteri = new Musteri
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        AdSoyad = reader["AdSoyad"].ToString(),
                        TelNo = reader["TelNo"].ToString(),
                        TcNo = reader["TcNo"].ToString(),
                        OdemeTuru = reader["OdemeTuru"].ToString()
                    };

                    musteriler.Add(musteri);
                }

                reader.Close();
            }

            return musteriler;
        }
    }
}
