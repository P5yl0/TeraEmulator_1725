using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commons
{
    public class Funcs
    {
        //pause (in milliSeconds)
        public static DateTime pause(int MilliSecondsToPauseFor)
        {
            if (MilliSecondsToPauseFor < 0) MilliSecondsToPauseFor = 50;

            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MilliSecondsToPauseFor);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                Application.DoEvents();
                ThisMoment = System.DateTime.Now;
            }
            return System.DateTime.Now;
        }

        //Delete File
        public static bool deleteFile(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
