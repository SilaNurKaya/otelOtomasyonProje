using odev2.DAL;
using odev2.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev2.Bussines
{
    public class YoneticiBussiness
    {
        public string SifreGetir(string guvenlikSorusu, string guvenlikCevabi)
        {
            YoneticiDAL yoneticiDal = new YoneticiDAL(); // YoneticiDAL nesnesi oluşturulur
            return yoneticiDal.SifreGetir(guvenlikSorusu, guvenlikCevabi);
        }

        public bool KullaniciGirisYap(string kullaniciAdi, string sifre)
        {
            // YoneticiDAL sınıfından Yonetici verilerini alıyoruz
            YoneticiDAL yoneticiDAL = new YoneticiDAL();
            YoneticiEntityLayer yonetici = yoneticiDAL.GirisKontrol(kullaniciAdi, sifre);

            // Eğer Yonetici objesi null değilse yani kullanıcı adı ve şifre veritabanında mevcutsa giriş başarılı demektir
            if (yonetici != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
