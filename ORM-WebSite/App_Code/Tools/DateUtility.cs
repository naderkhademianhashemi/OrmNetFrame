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

    public static string ToShamsi(this string filldate)
    {
        var farsiDate = string.Empty;
        var pDate = new PersianCalendar();
        if (filldate != string.Empty)
        {
            var datefilldate = Convert.ToDateTime(filldate);
            string year = Convert.ToString(pDate.GetYear(datefilldate));
            string month = String.Format(Convert.ToString(pDate.GetMonth(datefilldate)), "00");
            string day = String.Format(Convert.ToString(pDate.GetDayOfMonth(datefilldate)), "00");
            farsiDate = year + "/" + month + "/" + day;
        }
        return farsiDate;
    }

    public static DateTime ToMiladi(this string dateFA)
    {
        var stringArr = dateFA.Split('/');
        var year = Convert.ToInt32(stringArr[0]);
        var month = Convert.ToInt32(stringArr[1]);
        var day = Convert.ToInt32(stringArr[2]);
        return new DateTime(year, month, day, new PersianCalendar());
    }
}
