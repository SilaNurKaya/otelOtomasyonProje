using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev2.DAL
{
    public class DbBaglanti
    {
        public static MySqlConnection BaglantiGetir()
        {
            string connectionString = "Server=172.21.54.253;Database=25_132330013;Uid=25_132330013;Pwd=!nif.ogr13KA;";
            try
            {
                return new MySqlConnection(connectionString); // Bağlantıyı burada açma, sadece bağlantı nesnesini döndür.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı oluşturulamadı: " + ex.Message);
                throw;
            }
        }
    }
}
