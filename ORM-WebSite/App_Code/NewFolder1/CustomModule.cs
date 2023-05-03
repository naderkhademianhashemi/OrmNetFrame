using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;

public class CustomModule : IHttpModule
{
    public CustomModule()
    {
        // Constructor
    }
    public void Init(HttpApplication app)
    {
        app.BeginRequest += new EventHandler(BeginRequest);
    }
    public void BeginRequest(object source, EventArgs e)
    {

        HttpApplication app = (HttpApplication)source;
        HttpContext cont = app.Context;
        string notification = cont.CurrentNotification.ToString();
        string postNotification = cont.IsPostNotification.ToString();
        cont.Response.Headers.Set("CustomHeader2", "ASPX, Event = " + notification +
                ", PostNotification = " + postNotification +
                ", DateTime = " + DateTime.Now.ToString());

    }
    public void Dispose()
    {
    }
}