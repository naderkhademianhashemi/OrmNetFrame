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
using System.Data.Sql;
using System.IO;


public partial class FormView : System.Web.UI.Page
{
    int Form1 = 0;
    public String GetFid(object obj)
    { 
        return "ShowForm.aspx?Fid="+ obj.ToString();
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            binddata();
    }

    private void binddata()
    {
        GridView_Forms.DataSource = ORM.Forms.get_forms("", "", "");
        GridView_Forms.DataBind();
        String[] keys = { "FormID" };
        GridView_Forms.DataKeyNames = keys;
    }
  
    public String GetBoundlink(object ind)
    {
        return "javascript:myRef=window.open('Parameter_with_form.aspx?Fid="
            + ind.ToString()
            + "','وابستگی', 'width=500,height=400,scrollbars=yes');myRef.focus();";
    }

    protected void ImageButton_Search_Click(object sender, EventArgs e)
    {
        if (CheckBox_All_Location.Checked && CheckBox_field_Select_All.Checked)
            GridView_Forms.DataSource = ORM.Forms.get_forms("", "", "");
        else
            if (!CheckBox_All_Location.Checked && CheckBox_field_Select_All.Checked) ;
        else
                if (!CheckBox_field_Select_All.Checked && CheckBox_All_Location.Checked)
            GridView_Forms.DataSource = ORM.Forms.get_forms("", DropDownList_field.SelectedValue, "");

        if (!CheckBox_All_Location.Checked && !CheckBox_field_Select_All.Checked) ;

        GridView_Forms.DataBind();
    }
    protected void CheckBox_All_Location_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void CheckBox_field_Select_All_CheckedChanged(object sender, EventArgs e)
    {
        DropDownList_field.Enabled = !CheckBox_field_Select_All.Checked;
    }
    
    protected void GridView_Forms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditF")
        {
            Form1 = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView_Forms.Rows[Form1];
            string fid = row.Cells[0].Text.ToString();
            Response.Redirect("EditForm.aspx?FID=" + fid + "", true);
        }
        else if (e.CommandName == "Question")
        {
            Form1 = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("NewQuestion.aspx?FID=" + Form1 + "", true);
        }
        else if (e.CommandName == "View")
        {
            Form1 = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView_Forms.Rows[Form1];
            string View = row.Cells[0].Text.ToString();
            HttpContext.Current.Session.Clear();
            Response.Redirect("ViewQuestion.aspx?FID=" + View + "", true);
        }
        else if (e.CommandName == "Delete")
        {
            String FID = e.CommandArgument.ToString();
            DataTable dt = ORM.Forms.Get_Dt_Related_Parameter(FID);
            if (dt == null || dt.Rows.Count == 0)
            {
                ORM.Form_Instance.form_delete(FID);
                ORM.Forms.delete(FID);
                binddata();
            }
            else
                Response.Write("<script>alert('به علت استفاده از این فرم در سایر پارامترها امکان حذف این فرم نمی باشد برای حذف فرم ابتدا باید پارامترهای مرتبط را حذف نمایید')</script>");
        }
        else if (e.CommandName == "Relation")
            Form1 = Convert.ToInt32(e.CommandArgument);
    }
    protected void GridView_Forms_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView_Forms.EditIndex = e.NewEditIndex;
        binddata();
    }
    protected void GridView_Forms_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView_Forms.EditIndex = -1;
        binddata();
    }
    protected void GridView_Forms_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String Fid = GridView_Forms.DataKeys[GridView_Forms.EditIndex].Value.ToString();
        ORM.Forms form = new ORM.Forms(Fid);
        form.Form_Name = ((TextBox)GridView_Forms.Rows[GridView_Forms.EditIndex].FindControl("TextBox_Edit_Name")).Text ;
        form.Form_Description = ((TextBox) GridView_Forms.Rows[GridView_Forms.EditIndex].FindControl("TextBox_Edit_Description")).Text;
        GridView_Forms.EditIndex = -1;
        form.Update();
        binddata();
    }
    protected void GridView_Forms_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    protected void GridView_Forms_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Forms.PageIndex = e.NewPageIndex;
        binddata();
    }

    protected void GridView_Forms_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView_Forms_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
}
