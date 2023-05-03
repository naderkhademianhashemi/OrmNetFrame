using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;


public static class Alert
{
    static StackTrace _stackTrace = new StackTrace();
    public static void Show()
    {
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(
            HttpContext.Current.CurrentHandler as Page, 
            typeof(Alert), "Alert", 
            "alert('" + _stackTrace.GetFrame(1).GetMethod().Name + "');", 
            true);
    }
}



