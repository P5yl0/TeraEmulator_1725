using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commons
{
    public class Functions
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
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

    }
}
