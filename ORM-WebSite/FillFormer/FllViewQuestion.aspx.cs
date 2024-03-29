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
using RSRC;

public partial class ViewQuestion : System.Web.UI.Page
{

    String mode;
    bool completepage = true;
    bool numquestion = false;
    bool noquestion = false;
    bool nooption = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        var FID = Request.QueryString["Fid"];
        var Ansid = Request.QueryString["AnsID"];
        if (!ORM.Group_Permissions.Get_Permission(FID, RSUsers.risk)._writable)
            Response.Redirect("error.aspx", true);
        else
        {
            var F = new ORM.HTML_Forms(new ORM.Forms(FID), this);
            Panel1.Controls.Add(F.Get_Html());
            var fid = 0L;

            if (FID == null
                || !Int64.TryParse(FID, out fid)
                || (Ansid != null
                && !Int64.TryParse(Ansid, out fid)))
                Response.Redirect("homepage.aspx", true);

            if (FID == null)
                return;

            FID = FID.ToString();
            var FRM = new ORM.Forms(FID);
            lblFname.Text = FRM.Form_Name;
        }
    }

    Literal GetLiteral(String litstr)
    {
        Literal l = new Literal();
        l.Text = litstr;
        return l;
    }

    void AddFirstSection()
    {
        Panel1.Controls.Add(GetLiteral("<Table width='100%' style='Border:solid 1px #0676c4;background-color:#d0e4f2;'><tr><td>"));
    }

    void AddLastSection()
    {
        Panel1.Controls.Add(GetLiteral("</td></tr></table><br/>"));
    }

    int Select(ListItemCollection licol, String val)
    {
        for (int i = 0; i < licol.Count; i++)
        {
            if (licol[i].Value == val)
            {
                licol[i].Selected = true;
                return i;
            }
        }
        return -1;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        String FID = Request.QueryString["fid"];
        ORM.HTML_Forms F = new ORM.HTML_Forms(new ORM.Forms(FID), this);
        if (F.register_form(Panel1, FID, RSUsers.risk))
            Response.Redirect("ViewQuestionResult.aspx?fid=" + Request.QueryString["fid"]);
        else
            Alert.Show();
    }
}
