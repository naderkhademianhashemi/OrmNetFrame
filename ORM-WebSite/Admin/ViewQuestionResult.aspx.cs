using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class ViewQuestionResult : System.Web.UI.Page
{
    protected string type = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       if (Request.QueryString["type"] == "1") 
        Response.Redirect("ViewQuestion.aspx?FID=" + Request.QueryString["fid"]);
       else if (Request.QueryString["type"] == "2")
        Response.Redirect("ShowForm.aspx?FID=" + Request.QueryString["fid"]);
    }
}
