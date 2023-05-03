using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;


    public static class DateUtility
    {
        public static string MiladiToShamsi(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}_{1}_{2}", pc.GetYear(date),
                pc.GetMonth(date), pc.GetDayOfMonth(date));
        }
    }
