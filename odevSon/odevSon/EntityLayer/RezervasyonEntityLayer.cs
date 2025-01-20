using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static odev2.EntityLayer.MusteriEntityLayer;
using static odev2.EntityLayer.OdaEntityLayer;

namespace odev2.EntityLayer
{
    public class RezervasyonEntityLayer
    {
        public class Rezervasyon
        {
            public int RezervasyonId { get; set; }
            public int MusteriId { get; set; }
            public int OdaId { get; set; }
            public DateTime GirisTarihi { get; set; }
            public DateTime CikisTarihi { get; set; }

            // Navigasyon özellikleri
            public virtual Musteri Musteri { get; set; }
            public virtual Oda Oda { get; set; }
        }
    }
}
