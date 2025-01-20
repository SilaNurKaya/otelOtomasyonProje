using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev2.EntityLayer
{
    public class MusteriEntityLayer
    {
        public class Musteri
        {
            public int Id { get; set; }
            public string AdSoyad { get; set; }
            public string TelNo { get; set; }
            public string TcNo { get; set; }
            public string OdemeTuru { get; set; }
        }
    }
}
