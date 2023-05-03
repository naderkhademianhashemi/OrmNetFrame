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
using System.Data.SqlClient;
using RSRC;

public partial class _FillFormer_Forms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        GridView_Forms.DataSource = ORM.Forms.get_Forms_By_Permission(RSUsers.risk);
        GridView_Forms.DataBind();
    }

    
    public string ConcatArgs(object arg1, object arg2)
    {
        return arg1.ToString() + ";" + arg2.ToString();
    }

    public String GetFid(object obj)
    {
        return "FillFormerShowForm.aspx?Fid=" + obj.ToString();
    }

    protected void lbtnSend_Click(object sender, EventArgs e)
    {
        LinkButton bt = sender as LinkButton;
        string FormID = bt.CommandArgument.ToString();
        lblID.Text = FormID;
        //for response redirect with id to other pages
        Response.Redirect("FillFormerShowForm.aspx?FID=" + FormID);

    }


    protected void GridView_Forms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
            Response.Redirect("FllViewQuestion.aspx?FID=" + e.CommandArgument.ToString());
        if (e.CommandName == "NewQuestion")
        {
            //NewQuestion
            string[] arrayArgs = e.CommandArgument.ToString().Split(';');
            Response.Redirect("FillFormerNewQuestion.aspx?FID=" + arrayArgs[0]);

        }
    }
}
