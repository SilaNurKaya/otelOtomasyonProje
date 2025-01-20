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
using static odev2.EntityLayer.OdaEntityLayer;

namespace odev2.DAL
{
    public class OdaDAL
    {
        public List<Oda> OdaListele()
        {
            List<Oda> odalar = new List<Oda>();

            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "SELECT * FROM Odalar";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    odalar.Add(new Oda
                    {
                        OdaId = Convert.ToInt32(reader["oda_id"]),
                        OdaNo = Convert.ToInt32(reader["oda_no"]),
                        OdaTuru = reader["oda_turu"].ToString(),
                        OdaFiyati = Convert.ToDecimal(reader["oda_fiyati"])
                    });
                }
            }

            return odalar;
        }

        // 2. Boş odaları listele
        public List<Oda> BosOdalariListele(DateTime girisTarihi, DateTime cikisTarihi, string odaTuru)
        {
            List<Oda> bosOdalar = new List<Oda>();

            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = @"SELECT * FROM Odalar o
                             WHERE NOT EXISTS (
                                 SELECT 1 FROM Rezervasyonlar r
                                 WHERE r.oda_id = o.oda_id
                                 AND (r.giris_tarihi < @CikisTarihi AND r.cikis_tarihi > @GirisTarihi)
                             )
                             AND o.oda_turu = @OdaTuru";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@GirisTarihi", girisTarihi);
                command.Parameters.AddWithValue("@CikisTarihi", cikisTarihi);
                command.Parameters.AddWithValue("@OdaTuru", odaTuru);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bosOdalar.Add(new Oda
                    {
                        OdaId = Convert.ToInt32(reader["oda_id"]),
                        OdaNo = Convert.ToInt32(reader["oda_no"]),
                        OdaTuru = reader["oda_turu"].ToString(),
                        OdaFiyati = Convert.ToDecimal(reader["oda_fiyati"])
                    });
                }
            }

            return bosOdalar;
        }

        // 3. Yeni oda ekle
        public bool OdaEkle(Oda yeniOda)
        {
            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "INSERT INTO Odalar (oda_no, oda_turu, oda_fiyati) VALUES (@OdaNo, @OdaTuru, @OdaFiyati)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OdaNo", yeniOda.OdaNo);
                command.Parameters.AddWithValue("@OdaTuru", yeniOda.OdaTuru);
                command.Parameters.AddWithValue("@OdaFiyati", yeniOda.OdaFiyati);
                connection.Open();

                return command.ExecuteNonQuery() > 0; // Etkilenen satır sayısı > 0 ise başarılı
            }
        }

        // 4. Oda bilgilerini güncelle
        public bool OdaGuncelle(Oda guncellenenOda)
        {
            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "UPDATE Odalar SET oda_no = @OdaNo, oda_turu = @OdaTuru, oda_fiyati = @OdaFiyati WHERE oda_id = @OdaId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OdaId", guncellenenOda.OdaId);
                command.Parameters.AddWithValue("@OdaNo", guncellenenOda.OdaNo);
                command.Parameters.AddWithValue("@OdaTuru", guncellenenOda.OdaTuru);
                command.Parameters.AddWithValue("@OdaFiyati", guncellenenOda.OdaFiyati);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        // 5. Oda sil
        //Oda iptal nedenini buraya eklemeyi unutma
        public bool OdaSil(int odaNo)
        {
            using (var connection = DbBaglanti.BaglantiGetir())
            {
                string query = "DELETE FROM Odalar WHERE oda_no = @OdaNo";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OdaNo", odaNo);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}

