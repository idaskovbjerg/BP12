using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpNFCApp
{
    class Measurement
    {
        //Year, Month, Day, Hour, Minute, Second, Systolic, Diastolic, Mean, Pulse, Flags
        // Flag is not used

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }   
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public int Mean { get; set; }
        public int Pulse { get; set; }
    }
}
