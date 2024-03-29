using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using PersianDateControls;
using System.Activities.Expressions;

public partial class ViewQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String FID = Request.QueryString["Fid"];
        if (FID == null)
        {
            Panel1.Controls.Add(ORM.HTML_Forms.Get_Lbl());
            return;
        }
        string Ansid = Request.QueryString["ansid"];

        ORM.HTML_Forms thisform = new ORM.HTML_Forms(new ORM.Forms(FID), this);
        Panel1.Controls.Add(thisform.Get_Html());
        Int64 fid = 0;
        if (FID == null
            || !Int64.TryParse(FID, out fid)
            || (Ansid != null && !Int64.TryParse(Ansid, out fid)))
            Response.Redirect("homepage.aspx", true);

        ORM.Forms thisform1 = new ORM.Forms(FID.ToString());
        lblFname.Text = thisform1.Form_Name;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        String FID = Request.QueryString["fid"];
        String TYPE = "1";
        ORM.HTML_Forms FRM = new ORM.HTML_Forms(
            new ORM.Forms(FID), this);
        if (FRM.register_form(Panel1, FID, RSRC.RSUsers.risk))
            Response.Redirect("ViewQuestionResult.aspx?fid=" + 
                Request.QueryString["fid"] + "&type=" + TYPE);
        else
            Alert.Show();
    }
}
