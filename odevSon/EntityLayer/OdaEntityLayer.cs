using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev2.EntityLayer
{
    public class OdaEntityLayer
    {
        public class Oda
        {
            public int OdaId { get; set; }
            public string OdaTuru { get; set; }
            public int OdaNo { get; set; }
            public bool Durum { get; set; }
            public decimal OdaFiyati { get; set; }
        }
    }

}
