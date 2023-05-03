using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public int SessionLengthMinutes
    {
        get { return Session.Timeout; }
    }
    public string SessionExpireDestinationUrl
    {
        get { return "/Login.aspx"; }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        this.Head1.Controls.Add(new LiteralControl(
            String.Format(
                "<meta http-equiv='refresh' content='{0};url={1}'>",
            SessionLengthMinutes * 60, SessionExpireDestinationUrl)));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var TmpSessionTimeOut = Session.Timeout;
        var TmpSession = HttpContext.Current.Session;
        var TmpSessionStart = Session["SessionStart"] as DateTime?;
        if (TmpSessionStart.HasValue)
        {
            var mpSessionStartValue = TmpSessionStart.Value;
        }
        var str = AccssDb.code.AdminTools.GetSettingByField("sTitle");
        
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {

    }
}
