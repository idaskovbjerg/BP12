using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpNFCApp
{
    static class MeasurementService
    {
        public static List<Measurement> ReadFile(string filepath)
        {
            List<Measurement> data = null;
            try
            {
                var lines = File.ReadAllLines(filepath);

                var data2 = from l in lines
                           let split = l.Split(',')
                           select new Measurement
                           {
                               // Skip variable number 5 and 7 as they are not used for anything.
                               Year = int.Parse(split[0]),
                               Month = int.Parse(split[1]),
                               Day = int.Parse(split[2]),
                               Hour = int.Parse(split[3]),
                               Minute = int.Parse(split[4]),
                               Second = int.Parse(split[5]),
                               Systolic = int.Parse(split[6]),
                               Diastolic = int.Parse(split[7]),
                               Mean = int.Parse(split[8]),
                               Pulse = int.Parse(split[9]),
                           };

                data = (List<Measurement>)(data2.ToList());
            }
            catch { }

            return data;
        }
    }
}
