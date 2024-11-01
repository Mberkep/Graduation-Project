using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ArduinoData
    {
        public string Status { get; set; } // Kart doğrulama mesajı
        public int[] PinStates { get; set; }

        public ArduinoData()
        {
            PinStates = new int[7]; // 7 pin (2'den 8'e kadar) için yer ayır
        }
    }
}
