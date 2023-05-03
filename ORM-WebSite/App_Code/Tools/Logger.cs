using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


public static class Logger
{
    static string _Address = HttpContext.Current.Server.MapPath(string.Format("/Logs/"));
    static string _fileAddress = 
        Path.Combine(_Address, 
            string.Format("{0}.txt", DateUtility.MiladiToShamsi(DateTime.Now)));

    public static void Error(Exception ex)
    {
        if (!Directory.Exists(_Address))
            Directory.CreateDirectory(_Address);
        string message = string.Format("Error====>DateTime:{0}\n=====Exception:{1}", DateTime.Now, ex.ToString());
        File.AppendAllLines(_fileAddress, new List<string> { message });
    }

    public static void Error(Exception ex, string msg)
    {
        if (!Directory.Exists(_Address))
            Directory.CreateDirectory(_Address);
        string message = string.Format("Error====>DateTime:{0}=====Exception:{1}", DateTime.Now, ex.ToString());
        File.AppendAllLines(_fileAddress, new List<string> { message });
    }

    public static void Info(string msg, string place)
    {
        if (!Directory.Exists(_Address))
            Directory.CreateDirectory(_Address);
        string message = string.Format("Info====>DateTime:{0}\n=====Place:{1}\n=====Message:{2}",
             DateTime.Now, place, msg);
        File.AppendAllLines(_fileAddress, new List<string> { message });
    }
}
