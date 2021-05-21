using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Single
    {
        private static Single single;
        public string KullaniciAdi { get; set; }
        private Single()
        {

        }

        public static Single GetSingle()
        {
            if(single == null)
            {
                single = new Single();

            }
            return single;
        }
    }
}
