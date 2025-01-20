using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev2.EntityLayer
{
    public class YoneticiEntityLayer
    {
        public int YoneticiId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string GuvenlikSorusu { get; set; }
        public string GuvenlikCevabi { get; set; }
    }
}
