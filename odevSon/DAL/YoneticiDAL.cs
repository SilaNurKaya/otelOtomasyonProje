using MySql.Data.MySqlClient;
using odev2.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static odev2.EntityLayer.YoneticiEntityLayer;

namespace odev2.DAL
{
    public class YoneticiDAL
    {
        public void YoneticiEkle(int yoneticiId, string kullaniciAdi, string sifre, string guvenlikSorusu, string guvenlikCevabi)
        {
            DbBaglanti dbBaglanti = new DbBaglanti();

            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "INSERT INTO Yonetici (kullanici_Adi, sifre, guvenlik_Sorusu, guvenlik_Cevabi) VALUES (@KullaniciAdi, @Sifre, @GuvenlikSousu, @GuvenlikCevabi)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                command.Parameters.AddWithValue("@Sifre", sifre);
                command.Parameters.AddWithValue("@GuvenlikSousu", guvenlikSorusu);
                command.Parameters.AddWithValue("@GuvenlikCevabi", guvenlikCevabi);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Yönetici eklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        public void YoneticiSil(int kullaniciAdi)
        {
            DbBaglanti dbBaglanti = new DbBaglanti();

            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "DELETE FROM Yonetici WHERE KullaniciAdi = @KullaniciAdi";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Yönetici silinirken bir hata oluştu:" + ex.Message);
                }

            }
        }

        public List<YoneticiEntityLayer> YoneticiListele()
        {
            List<YoneticiEntityLayer> yoneticiler = new List<YoneticiEntityLayer>();

            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "SELECT * FROM Yonetici";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yoneticiler.Add(new YoneticiEntityLayer
                    {
                        KullaniciAdi = reader.GetString("kullanici_adi"),
                        Sifre = reader.GetString("sifre"),
                        GuvenlikSorusu = reader.GetString("guvenlik_sorusu"),
                        GuvenlikCevabi = reader.GetString("guvenlik_cevabi")
                    });
                }
            }

            return yoneticiler;
        }

        public void YoneticiGuncelle(YoneticiEntityLayer yonetici)
        {
            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "UPDATE Yonetici SET kullanici_adi = @KullaniciAdi, sifre = @Sifre, guvenlik_sorusu = @GuvenlikSorusu, guvenlik_cevabi = @GuvenlikCevabi WHERE id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", yonetici.KullaniciAdi);
                command.Parameters.AddWithValue("@Sifre", yonetici.Sifre);
                command.Parameters.AddWithValue("@GuvenlikSorusu", yonetici.GuvenlikSorusu);
                command.Parameters.AddWithValue("@GuvenlikCevabi", yonetici.GuvenlikCevabi);
                command.Parameters.AddWithValue("@Id", yonetici.YoneticiId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public string SifreGetir(string guvenlikSorusu, string guvenlikCevabi)
        {
            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "SELECT sifre FROM Yonetici WHERE guvenlik_sorusu = @GuvenlikSorusu AND guvenlik_cevabi = @GuvenlikCevabi";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@GuvenlikSorusu", guvenlikSorusu);
                command.Parameters.AddWithValue("@GuvenlikCevabi", guvenlikCevabi);

                connection.Open();
                var result = command.ExecuteScalar();

                return result != null ? result.ToString() : string.Empty;
            }
        }



        public YoneticiEntityLayer GirisKontrol(string kullaniciAdi, string sifre)
        {
            YoneticiEntityLayer yonetici = null;

            using (MySqlConnection connection = DbBaglanti.BaglantiGetir())
            {
                string query = "SELECT * FROM Yonetici WHERE kullanici_Adi = @KullaniciAdi AND sifre = @Sifre";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                command.Parameters.AddWithValue("@Sifre", sifre);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    // Veritabanından kullanıcı bilgileri varsa, Yonetici objesini oluşturuyoruz
                    if (reader.Read())
                    {
                        yonetici = new YoneticiEntityLayer
                        {
                            KullaniciAdi = reader["kullanici_Adi"].ToString(),
                            Sifre = reader["sifre"].ToString(),
                            GuvenlikSorusu = reader["guvenlik_Sorusu"].ToString(),
                            GuvenlikCevabi = reader["guvenlik_Cevabi"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Veritabanı hatası: " + ex.Message);
                }
            }
            return yonetici; // Yonetici objesini döndürüyoruz
        }
    }
}
