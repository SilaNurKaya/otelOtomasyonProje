using odev2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static odev2.EntityLayer.MusteriEntityLayer;

namespace odev2.Bussines
{
    public class MusteriBusiness
    {
        public MusteriDAL dal = new MusteriDAL();

        public void MusteriEkle(Musteri musteri)
        {
            if (!string.IsNullOrEmpty(musteri.TcNo) && musteri.TcNo.Length == 11)
            {
                int musteriTelNoInt = Convert.ToInt32(musteri.TelNo);
                int musteriTcNoInt = Convert.ToInt32(musteri.TcNo);

                dal.MusteriEkle(musteri.AdSoyad, musteriTelNoInt, musteriTcNoInt, musteri.OdemeTuru);
            }
            else
            {
                throw new Exception("TC Kimlik No 11 karakter olmalı!");
            }
        }

        public void MusteriGuncelle(Musteri musteri)
        {
            if (!string.IsNullOrEmpty(musteri.TcNo))
            {
                int musteriyeniTelNoInt = Convert.ToInt32(musteri.TelNo);
                int musteriTcNoInt = Convert.ToInt32(musteri.TcNo);

                dal.MusteriGuncelle(musteri.AdSoyad, musteriyeniTelNoInt, musteriTcNoInt, musteri.OdemeTuru);
            }
            else
            {
                throw new Exception("Geçerli bir TC Kimlik No giriniz!");
            }
        }

        public void MusteriSil(string tcNo)
        {
            if (!string.IsNullOrEmpty(tcNo))
            {
                dal.MusteriSil(tcNo);
            }
            else
            {
                throw new Exception("Geçerli bir TC Kimlik No giriniz!");
            }
        }

        public List<Musteri> MusteriListele()
        {
            return dal.MusteriListele();


        }
    }

}
